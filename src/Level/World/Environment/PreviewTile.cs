using Blip.src.Engine.Level;
using Blip.src.Engine.Sprite;
using Blip.src.Level.Scenes;
using Blip.src.Level.Scenes.DataDriven;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        string filePath = Path.Combine("..", "..", "..", "Content", "assets", "scenes", "world.json");

        DataDrivenScene dataDrivenScene = JsonSerializer.Deserialize<DataDrivenScene>(File.ReadAllText(filePath));

        if (currentMouseState.LeftButton == ButtonState.Pressed)
        {
            placementTileGrid.SetTileWithWorld(new Tile(scene, Vector2.Zero, identifier), position);

            dataDrivenScene.Grids[0].Tiles.Add(new DataDrivenScene.Tile((int)position.X / 16, (int)position.Y / 16, identifier));

            string jsonString = JsonSerializer.Serialize(dataDrivenScene);

            File.WriteAllText(filePath, jsonString);
        }

        if (currentMouseState.RightButton == ButtonState.Pressed)
        {
            placementTileGrid.RemoveTileWithWorld(position);
            dataDrivenScene.Grids[0].Tiles.RemoveAll(tile => tile.X == position.X / 16 && tile.Y == position.Y / 16);

            string jsonString = JsonSerializer.Serialize(dataDrivenScene);

            File.WriteAllText(filePath, jsonString);
        }

    }

    public override void Draw(SpriteBatch levelSpriteBatch, float layer = 1f)
    {
        if (scene.gameStateManager.GetGameState() == Manager.GameState.GameState.Play) return;
        base.Draw(levelSpriteBatch, layer);
    }
}
