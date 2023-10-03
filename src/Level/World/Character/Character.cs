using Blip.src.Engine.Sprite;
using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.World.Character;
public abstract class Character
{
    public string identifier;

    public Sprite2d sprite;

    public Vector2 position;

    protected Scene scene;
    
    public Character(Scene scene, string identifier, Vector2 position)
    {
        this.scene = scene;
        this.identifier = identifier;
        this.sprite = new Sprite2d(scene.gameStateManager.content, scene.gameStateManager.graphicsDevice, identifier, position);
        this.position = position;
    }

    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch levelSpriteBatch, float layer)
    {
        sprite.Draw(levelSpriteBatch, layer);
    }
}
