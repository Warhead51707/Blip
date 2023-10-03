using Blip.src.Engine.Level;
using Blip.src.Engine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blip.src.Engine.UI;
public class UIManager
{
    public Camera uiCamera;
    public GraphicsAdapter graphicsAdapter;
    public GraphicsDevice graphicsDevice;
    public ContentManager contentManager;
 
    private SpriteBatch uiSpriteBatch;

    private List<UIElement> ui = new List<UIElement>();

    public UIManager(GraphicsDevice graphicsDevice, Camera uiCamera, GraphicsAdapter graphicsAdapter, ContentManager contentManager)
    {
        uiSpriteBatch = new SpriteBatch(graphicsDevice);

        this.uiCamera = uiCamera;
        this.graphicsAdapter = graphicsAdapter;
        this.graphicsDevice = graphicsDevice;
        this.contentManager = contentManager;
    }

    public void AddUIElement(UIElement uiElement)
    {
        ui.Add(uiElement);
    }

    public void RemoveUIElement(UIElement uiElement)
    {
          ui.Remove(uiElement);
    }

    public T GetUIElement<T>(string identifier) where T : UIElement
    {
        foreach (UIElement uiElement in ui)
        {
            if (uiElement.identifier == identifier)
            {
                return (T)uiElement;
            }
        }

        return null;
    }

    public void Update(GameTime gameTime)
    {
        foreach (UIElement uiElement in ui)
        {
            if (!uiElement.visible) continue;
           // Debug.WriteLine(uiElement.identifier);
            uiElement.Update();
        }
    }

    public void Draw()
    {
        uiSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, uiCamera.uiTransformMatrix);

        foreach (UIElement uiElement in ui)
        {
            if (!uiElement.visible) continue;

            uiElement.Draw(uiSpriteBatch);
        }

        uiSpriteBatch.End();
    }
}
