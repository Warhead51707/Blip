using Blip.src.Engine.Sprite;
using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blip.src.Level.World.Environment;
public class Tile
{
    public Vector2 position, gridPosition;
    
    public string identifier;

    public Sprite2d sprite;

    protected Scene scene;

    public Tile(Scene scene, Vector2 position, string identifier)
    {
        this.position = position;
        this.identifier = identifier;

        this.sprite = new Sprite2d(scene.gameStateManager.content, scene.gameStateManager.graphicsDevice, identifier != "none" ? identifier : "tiles/testTile", position);
    }

    public virtual void Update()
    {
        sprite.position = position;
    }

    public virtual void Draw(SpriteBatch levelSpriteBatch, float layer = 1f)
    {
        sprite.Draw(levelSpriteBatch, layer);
    }
}
