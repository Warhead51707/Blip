using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.World.Character.Player;
public class Player : Character
{
    private float movementSpeed = 50f;

    public Player(Scene scene, string identifier, Vector2 position) : base(scene, identifier, position)
    {
    }

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.W)) position.Y -= movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.S)) position.Y += movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.A)) position.X -= movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.D)) position.X += movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        UpdatePosition();

        scene.camera.Attach(position);
    }

    private void UpdatePosition()
    {
        sprite.position = position;
    }

    public override void Draw(SpriteBatch levelSpriteBatch, float layer)
    {
        base.Draw(levelSpriteBatch, layer);
    }
}
