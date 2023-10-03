using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class UIText : UIElement
{
    public string text;
    public Color color;
    public float scale;

    private SpriteFont pixelFont;
    private float layer;
    public UIText(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, string text, Color color, float scale = 1f, float layer = 2f) : base(identifier, uiManager, anchorPoint, offset, Vector2.One)
    {
        this.text = text;
        this.color = color;
        this.scale = scale;
        this.layer = layer * 0.001f;

        pixelFont = uiManager.contentManager.Load<SpriteFont>("assets/fonts/pixel");

        size = pixelFont.MeasureString(text);
    }

    public override void Update()
    {
        size.X = pixelFont.MeasureString(text).X / 2;
        size.Y = pixelFont.MeasureString(text).Y / -2;

        base.Update();
    }

    public override void Draw(SpriteBatch uiSpriteBatch)
    {

        uiSpriteBatch.DrawString(pixelFont, text, position, color, 0f, pixelFont.MeasureString(text) / 2, scale, SpriteEffects.None, layer);
    }
}
