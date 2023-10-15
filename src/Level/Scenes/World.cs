using Blip.src.Engine.Level;
using Blip.src.Level.Scenes.DataDriven;
using Blip.src.Level.World.Character.Player;
using Blip.src.Level.World.Environment;
using Blip.src.Manager.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blip.src.Level.Scenes;
public class World : Scene
{
    public Player player;
    public TileGrid tileGrid;

    public World(GameStateManager gameStateManager, Camera camera) : base(gameStateManager, camera)
    {
        player = new Player(this, "character/player/char1", new Vector2(0, 64));
        tileGrid = new TileGrid(this, 1f);
        
        string dataDrivenFileContents = System.IO.File.ReadAllText("Content/assets/scenes/world.json");
        DataDrivenScene dataDrivenScene = JsonSerializer.Deserialize<DataDrivenScene>(dataDrivenFileContents);

        foreach (DataDrivenScene.Grid grid in dataDrivenScene.Grids)
        {
            foreach (DataDrivenScene.Tile tile in grid.Tiles)
            {
                tileGrid.SetTileOnGrid(new Tile(this, Vector2.Zero, tile.Identifier), new Vector2(tile.X, tile.Y));
            }
        }

    }

    public override void Draw(SpriteBatch levelSpriteBatch)
    {
        player.Draw(levelSpriteBatch, 2f);
        tileGrid.Draw(levelSpriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        tileGrid.Update();
        
    }
}
