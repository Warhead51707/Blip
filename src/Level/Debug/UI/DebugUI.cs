using Blip.src.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Level.Debug.UI;
public class DebugUI
{
    private Panel topPanel;
    private Panel testPanel;
    private Panel tilePanel;
    private Button playButton;

    private bool isTilePlacer = false;
    public DebugUI(UIManager uiManager)
    {
        testPanel = new Panel("Test Panel", uiManager, AnchorPoint.BottomLeft, new Vector2(0, 0), new Vector2(148, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 3) - 20), "ui/test");
        topPanel = new Panel("Top Panel", uiManager, AnchorPoint.TopRight, new Vector2(-5, 0), new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3) + 10, 15), "ui/test3");
        tilePanel = new Panel("Tile Panel", uiManager, AnchorPoint.Mouse, new Vector2(-25, 65), new Vector2(40, 20), "ui/test");

        topPanel.AddChild(new UIText("File Text", uiManager, AnchorPoint.None, new Vector2(20, 7), "File", Color.Yellow));
        topPanel.AddChild(new UIText("Edit Text", uiManager, AnchorPoint.None, new Vector2(46, 7), "Edit", Color.Yellow));
        topPanel.AddChild(new UIText("Editor Text", uiManager, AnchorPoint.None, new Vector2(76, 7), "Editor", Color.Yellow));

        topPanel.AddChild(new UIText("FPS Text", uiManager, AnchorPoint.None, new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3) - 45, 7), "FPS: ", Color.Pink));

        playButton = new Button("Play Button", uiManager, AnchorPoint.BottomRight, new Vector2(16, 16), new Vector2(32, 32), "ui/test2", 2f);

        playButton.AddChild(new UIImage("Play Button Image", uiManager, AnchorPoint.None, new Vector2(13, 15), new Vector2(32, 32), "ui/play_button"));

        testPanel.AddChild(new UIText("Tile Placer Text", uiManager, AnchorPoint.None, new Vector2(74, 12), "Tile Placer", Color.White));

        GridLayout testLayout = new GridLayout("Test Layout", uiManager, AnchorPoint.None, new Vector2(8, 24), new Vector2(32, 32), 3, 48f);

        string[] tileDirectoryFiles = System.IO.Directory.GetFiles("Content/assets/sprites/tiles");

        for (int i = 0; i < tileDirectoryFiles.Length - 1; i++)
        {
            Button tileButton = new Button("Tile Button" + i, uiManager, AnchorPoint.None, new Vector2(0, 0), new Vector2(32, 32), "ui/test2", 2f);

            string fileName = Path.GetFileNameWithoutExtension(tileDirectoryFiles[i + 1]);

            tileButton.AddChild(new UIImage("Test Tile Image" + i, uiManager, AnchorPoint.None, new Vector2(16, 16), new Vector2(16, 16), "tiles/" + fileName));

            testLayout.AddChild(tileButton);
        }

        testPanel.AddChild(testLayout);

        tilePanel.AddChild(new UIText("Grid Pos Text", uiManager, AnchorPoint.None, new Vector2(20,10), "(0,0)", Color.White));

        tilePanel.setChild(true);

        tilePanel.visible = false;

        testPanel.visible = false;

        uiManager.AddUIElement(testPanel);
        uiManager.AddUIElement(topPanel);
        uiManager.AddUIElement(playButton);
        uiManager.AddUIElement(tilePanel);
    }

    public bool IsTilePlacer()
    {
        return isTilePlacer;
    }

    public void SetTilePlacer(bool isTilePlacer)
    {
        this.isTilePlacer = isTilePlacer;
    }

    public void HideDebugUI()
    {
        testPanel.visible = false;
        tilePanel.visible = false;
    }

    public void ShowDebugUI()
    {
        testPanel.visible = true;

        if (isTilePlacer) tilePanel.visible = true;
    }
}
