using Blip.src.Engine.Level;
using Blip.src.Engine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class Panel : UIElement
{
    public NineSlice panelBackground;

    public bool isHovering;
    private bool stoppedHovering;

    private float layer;

    private List<UIElement> children = new List<UIElement>();

    private MouseState currentMouseState;

    public Panel(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, Vector2 size, string backgroundSprite, float layer = 1f) : base(identifier, uiManager, anchorPoint, offset, size)
    {
        this.size = size;
        this.layer = layer;

        this.panelBackground = new NineSlice(SpriteHelper.createTextureFromPNG(backgroundSprite, uiManager.graphicsDevice), 2, new Vector4(0, 0, size.X, size.Y));
    }
    public override void Update()
    {

        MouseState lastMouseState = currentMouseState;

        currentMouseState = Mouse.GetState();

        base.Update();
        panelBackground.position.X = position.X;
        panelBackground.position.Y = position.Y;

        foreach (UIElement child in children)
        {
            child.position = Vector2.Add(position, child.offset);
            child.Update();
        }

        Vector2 mouseWorldPos = Vector2.Transform(currentMouseState.Position.ToVector2(), Matrix.Invert(uiManager.uiCamera.uiTransformMatrix));

        isHovering = mouseWorldPos.X > position.X && mouseWorldPos.X < position.X + size.X && mouseWorldPos.Y > position.Y && mouseWorldPos.Y < position.Y + size.Y;

        if (!stoppedHovering) uiManager.uiCamera.SetFocus(isHovering);

        stoppedHovering = !isHovering;

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        panelBackground.Draw(spriteBatch, layer);

        foreach (UIElement child in children)
        {
            child.Draw(spriteBatch);
        }
    }

    public void AddChild(UIElement child)
    {
        child.setChild(true);
        children.Add(child);
    }

    public T GetChild<T>(string identifier) where T : UIElement
    {
        foreach (UIElement child in children)
        {
            if (child.identifier == identifier)
            {
                return (T)child;
            }
        }

        return null;
    }

    public void ChangeBackground(string backgroundSprite)
    {
        panelBackground = new NineSlice(SpriteHelper.createTextureFromPNG(backgroundSprite, uiManager.graphicsDevice), 2, new Vector4(0, 0, size.X, size.Y));
    }
}
