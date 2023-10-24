using Blip.src.Engine.Sprite;
using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.World.Environment;
public class TileGrid
{
    public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    private float layer = 1f;

    private Scenes.World scene;

    public TileGrid(Scenes.World scene, float layer)
    {
        this.scene = scene;
        this.layer = layer;
    }

    public void SetTileWithWorld(Tile tile, Vector2 worldPosition)
    {
        worldPosition.X = (float)Math.Round(worldPosition.X / 16) * 16;
        worldPosition.Y = (float)Math.Round(worldPosition.Y / 16) * 16;

        tile.position = worldPosition;

        tiles[WorldPosToGridPos(worldPosition)] = tile;
    }

    public void SetTileOnGrid(Tile tile, Vector2 gridPosition)
    {
        tiles[gridPosition] = tile;

        tile.position.X = gridPosition.X * 16;
        tile.position.Y = gridPosition.Y * 16;
    }

    public Tile GetTileWithWorld(Vector2 worldPosition)
    {
        worldPosition.X = (float)Math.Round(worldPosition.X / 16) * 16;
        worldPosition.Y = (float)Math.Round(worldPosition.Y / 16) * 16;

        Vector2 gridPosition = WorldPosToGridPos(worldPosition);

        if (tiles.ContainsKey(gridPosition))
        {
            return tiles[gridPosition];
        }
        else
        {
            return null;
        }
    }

    public void RemoveTileWithWorld(Vector2 worldPosition)
    {
        worldPosition.X = (float)Math.Round(worldPosition.X / 16) * 16;
        worldPosition.Y = (float)Math.Round(worldPosition.Y / 16) * 16;

        tiles.Remove(WorldPosToGridPos(worldPosition));
    }

    public void RemoveTileWithGrid(Vector2 gridPosition)
    {
        tiles.Remove(gridPosition);
    }

    public void Update()
    {
        TileUpdates();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        TileUpdates(spriteBatch);
    }

    public void TileUpdates(SpriteBatch spriteBatch = null)
    {
        float cameraWidth = scene.gameStateManager.graphicsDevice.Viewport.Width / scene.camera.transformMatrix.M11;
        float cameraHeight = scene.gameStateManager.graphicsDevice.Viewport.Height / scene.camera.transformMatrix.M22;

        Vector2 cameraPosition = new Vector2(-scene.camera.transformMatrix.Translation.X, -scene.camera.transformMatrix.Translation.Y);

        Vector2 cameraGridPosition = WorldPosToGridPos(cameraPosition);

        int numTilesX = (int)Math.Round(cameraWidth / 16) + 2;
        int numTilesY = (int)Math.Round(cameraHeight / 16) + 2;


        int startX = (int)Math.Ceiling(cameraGridPosition.X / scene.camera.transformMatrix.M11) - 1;
        int startY = (int)Math.Ceiling(cameraGridPosition.Y / scene.camera.transformMatrix.M22) - 1;

        for (int y = 0; y < numTilesY; y++)
        {
            for (int x = 0; x < numTilesX; x++)
            {
                int tileX = startX + x;
                int tileY = startY + y;

                if (tiles.ContainsKey(new Vector2(tileX, tileY)))
                {
                    if (spriteBatch != null)
                    {
                        tiles[new Vector2(tileX, tileY)].Draw(spriteBatch, layer);
                        continue;
                    }

                    tiles[new Vector2(tileX, tileY)].Update();
                }
            }
        }
    }


    private Vector2 WorldPosToGridPos(Vector2 worldPosition)
    {
        return Vector2.Floor(new Vector2((int)(worldPosition.X / 16), (int)(worldPosition.Y / 16)));
    }
}
