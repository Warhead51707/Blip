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

    private Scene scene;

    public TileGrid(Scene scene, float layer)
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
        foreach (KeyValuePair<Vector2, Tile> tile in tiles)
        {
            tile.Value.Update();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (KeyValuePair<Vector2,Tile> tile in tiles)
        {
            tile.Value.Draw(spriteBatch, layer);
        }
    }


    private Vector2 WorldPosToGridPos(Vector2 worldPosition)
    {
        return Vector2.Floor(new Vector2((int)(worldPosition.X / 16), (int)(worldPosition.Y / 16)));
    }
}
