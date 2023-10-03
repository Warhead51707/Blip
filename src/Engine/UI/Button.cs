using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class Button : Panel
{
    public delegate void ClickEventHandler();

    public event ClickEventHandler OnClick;

    public int clicks;

    private MouseState currentMouseState;

    public Button(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, Vector2 size, string backgroundSprite, float layer = 1f) : base(identifier, uiManager, anchorPoint, offset, size, backgroundSprite, layer)
    {
    }

    public override void Update()
    {
        MouseState lastMouseState = currentMouseState;

        currentMouseState = Mouse.GetState();

        if (isHovering && currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
        {
            clicks++;
            OnClick?.Invoke();
        }
        base.Update();
    }

}
