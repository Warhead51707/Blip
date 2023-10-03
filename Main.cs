using Blip.src.Engine.Level;
using Blip.src.Engine.UI;
using Blip.src.Manager.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Blip
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch levelSpriteBatch;

        private GameStateManager gameStateManager;
        private Camera gameCamera;
        private UIManager uiManager;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60.0f);
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "BLIP";
            Window.AllowUserResizing = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.HardwareModeSwitch = false;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            levelSpriteBatch = new SpriteBatch(GraphicsDevice);
            gameCamera = new Camera(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);   
            uiManager = new UIManager(GraphicsDevice, gameCamera, GraphicsAdapter.DefaultAdapter, Content);
            gameStateManager = new GameStateManager(Content, uiManager, GraphicsDevice, gameCamera);

            gameCamera.addGameStateManager(gameStateManager);

            gameStateManager.ChangeGameState(GameState.Debug);
        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);
            uiManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            levelSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, gameCamera.transformMatrix);

            gameStateManager.Draw(levelSpriteBatch);

            levelSpriteBatch.End();

            uiManager.Draw();

            base.Draw(gameTime);
        }
    }
}