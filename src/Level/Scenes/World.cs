using Blip.src.Engine.Level;
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
using System.Threading.Tasks;

namespace Blip.src.Level.Scenes;
public class World : Scene
{
    public Player player;
    public TileGrid tileGrid;

    public World(GameStateManager gameStateManager, Camera camera) : base(gameStateManager, camera)
    {
        player = new Player(this, "character/player/byte", new Vector2(0, 64));
        tileGrid = new TileGrid(this, 1f);

        tileGrid.SetTileOnGrid(new Tile(this, Vector2.Zero, "tiles/testTile"), new Vector2(0, 0));
        tileGrid.SetTileOnGrid(new Tile(this, Vector2.Zero, "tiles/testTile"), new Vector2(1, 0));


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
