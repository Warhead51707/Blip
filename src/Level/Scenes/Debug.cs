using Blip.src.Engine.Level;
using Blip.src.Engine.UI;
using Blip.src.Level.Debug.UI;
using Blip.src.Level.World.Environment;
using Blip.src.Manager.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.Scenes;
public class Debug : World
{
    public bool isActive = true;

    private DebugUI debugUI;

    private TileGrid previewTileGrid;

    private PreviewTile previewTile;

    private MouseState currentMouseState;

    private Button tileButton;

    private Button playButton;

    public Debug(GameStateManager gameStateManager, Camera camera) : base(gameStateManager, camera)
    {
        debugUI = new DebugUI(gameStateManager.uiManager);

        previewTileGrid = new TileGrid(this, 3f);

        previewTile = new PreviewTile(this, Vector2.Zero, "none", tileGrid, previewTileGrid);

        tileButton = gameStateManager.uiManager.GetUIElement<Panel>("Test Panel").GetChild<Button>("Tile Button");
        playButton = gameStateManager.uiManager.GetUIElement<Button>("Play Button");

        tileButton.OnClick += ChangePreviewTile;
        playButton.OnClick += PlayButton;
    }

    private void ChangePreviewTile()
    {
        previewTile.ChangePreviewTile("tiles/testTile");

        Panel tilePanel = gameStateManager.uiManager.GetUIElement<Panel>("Tile Panel");

        tilePanel.visible = !tilePanel.visible;

        debugUI.SetTilePlacer(!debugUI.IsTilePlacer());
    }

    private void PlayButton()
    {
        UIImage playButtonImage = playButton.GetChild<UIImage>("Play Button Image");

        GameState currentGameState = gameStateManager.GetGameState();

        if (currentGameState == GameState.Debug)
        {
            playButtonImage.offset += new Vector2(3, 1);
            playButtonImage.ChangeSprite("ui/stop_button");
            debugUI.HideDebugUI();
            isActive = false;
            gameStateManager.ChangeGameState(GameState.Play);
        }

        if (currentGameState == GameState.Play)
        {
            playButtonImage.offset -= new Vector2(3, 1);
            playButtonImage.ChangeSprite("ui/play_button");
            debugUI.ShowDebugUI();
            isActive = true;
            gameStateManager.ChangeGameState(GameState.Debug);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (isActive)
        {
            camera.Update();
            previewTile.Update();
            tileGrid.Update();

            currentMouseState = Mouse.GetState();

            Vector2 worldMousePosition = Vector2.Transform(new Vector2(currentMouseState.X, currentMouseState.Y), Matrix.Invert(camera.transformMatrix));

            UIText gridPosText = gameStateManager.uiManager.GetUIElement<Panel>("Tile Panel").GetChild<UIText>("Grid Pos Text");

            worldMousePosition.X = (float)Math.Round(worldMousePosition.X / 16) * 16;
            worldMousePosition.Y = (float)Math.Round(worldMousePosition.Y / 16) * 16;

            Vector2 gridMousePosition = Vector2.Floor(new Vector2(worldMousePosition.X / 16, worldMousePosition.Y / 16));

            gridPosText.text = "(" + gridMousePosition.X + "," + gridMousePosition.Y + ")";

            return;
        }

        base.Update(gameTime);
 
    }

    public override void Draw(SpriteBatch levelSpriteBatch)
    {
        previewTileGrid.Draw(levelSpriteBatch);
        previewTile.Draw(levelSpriteBatch);
        base.Draw(levelSpriteBatch);
    }
}
