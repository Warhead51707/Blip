using Blip.src.Engine.Level;
using Blip.src.Engine.UI;
using Blip.src.Level.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blip.src.Manager.GameState;
public class GameStateManager
{
    public readonly ContentManager content;
    public readonly GraphicsDevice graphicsDevice;
    public readonly UIManager uiManager;

    public Camera camera;

    public Debug debug;

    private GameState gameState;

    public GameStateManager(ContentManager content, UIManager uIManager, GraphicsDevice graphicsDevice, Camera camera)
    {
        this.content = content;
        this.uiManager = uIManager;
        this.camera = camera;

        gameState = GameState.Play;
        this.graphicsDevice = graphicsDevice;
        debug = new Debug(this, camera);
    }

    public void Update(GameTime gameTime)
    {
       debug.Update(gameTime);
    }

    public void Draw(SpriteBatch levelSpriteBatch)
    {
       debug.Draw(levelSpriteBatch);
    }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
