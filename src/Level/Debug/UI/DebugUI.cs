﻿using Blip.src.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        topPanel = new Panel("Top Panel", uiManager, AnchorPoint.TopRight, new Vector2(0, 0), new Vector2(uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3, 20), "ui/test3");
        tilePanel = new Panel("Tile Panel", uiManager, AnchorPoint.Mouse, new Vector2(-25, 65), new Vector2(40, 20), "ui/test");

        topPanel.AddChild(new UIText("W Engine Text", uiManager, AnchorPoint.None, new Vector2(74, 10), "W ENGINE: 0.0.1", Color.Yellow));

        playButton = new Button("Play Button", uiManager, AnchorPoint.BottomRight, new Vector2(16, 16), new Vector2(32, 32), "ui/test2", 2f);

        playButton.AddChild(new UIImage("Play Button Image", uiManager, AnchorPoint.None, new Vector2(13, 15), new Vector2(32, 32), "ui/play_button"));

        testPanel.AddChild(new UIText("Tile Placer Text", uiManager, AnchorPoint.None, new Vector2(74, 12), "Tile Placer", Color.White));

        testPanel.AddChild(new Button("Tile Button", uiManager, AnchorPoint.None, new Vector2(14, 24), new Vector2(32, 32), "ui/test2", 2f));

        testPanel.AddChild(new UIImage("Test Tile Image", uiManager, AnchorPoint.None, new Vector2(30, 40), new Vector2(16, 16), "tiles/testTile"));

        tilePanel.AddChild(new UIText("Grid Pos Text", uiManager, AnchorPoint.None, new Vector2(20,10), "(0,0)", Color.White));

        tilePanel.setChild(true);

        tilePanel.visible = false;

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
