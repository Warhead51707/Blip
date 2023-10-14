using Blip.src.Engine.Level;
using Blip.src.Engine.Sprite;
using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.World.Environment;
public class PreviewTile : Tile
{

    private TileGrid placementTileGrid;
    private TileGrid previewTileGrid;

    private MouseState currentMouseState;

    public PreviewTile(Scene scene, Vector2 position, string identifier, TileGrid placementTileGrid, TileGrid previewTileGrid) : base(scene, position, identifier)
    {
        this.placementTileGrid = placementTileGrid;
        this.previewTileGrid = previewTileGrid;
        this.scene = scene;
    }

    public void ChangePreviewTile(string identifier)
    {
        this.identifier = identifier;
        sprite = new Sprite2d(scene.gameStateManager.content, scene.gameStateManager.graphicsDevice, identifier, position);
    }

    public override void Update()
    {
        previewTileGrid.tiles.Clear();

        base.Update();

        if (identifier == "none")
        {
            sprite.color = Color.Transparent;
            return;
        }

        if (scene.gameStateManager.camera.IsFocused()) return;

        MouseState previousMouseState = currentMouseState;

        currentMouseState = Mouse.GetState();

        Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

        position = Vector2.Transform(mousePosition, Matrix.Invert(scene.camera.transformMatrix));

        if (placementTileGrid.GetTileWithWorld(position) != null)
        {
            sprite.color = Color.Red;
        }
        else
        {
            sprite.color = Color.Green;
        }

        previewTileGrid.SetTileWithWorld(this, position);

        if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            placementTileGrid.SetTileWithWorld(new Tile(scene, Vector2.Zero, identifier), position);
        }

        if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
        {
            placementTileGrid.RemoveTileWithWorld(position);
        }

    }

    public override void Draw(SpriteBatch levelSpriteBatch, float layer = 1f)
    {
        if (scene.gameStateManager.GetGameState() == Manager.GameState.GameState.Play) return;
        base.Draw(levelSpriteBatch, layer);
    }
}
