using Blip.src.Engine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class UIImage : UIElement
{
    public Sprite2d sprite;
    public float layer;

    public UIImage(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, Vector2 size, string imagePath, float layer = 3f) : base(identifier, uiManager, anchorPoint, offset, size)
    {
        sprite = new Sprite2d(uiManager.contentManager, uiManager.graphicsDevice, imagePath, position);

        sprite.scale = new Vector2(size.X / sprite.texture2d.Bounds.Width, size.Y / sprite.texture2d.Bounds.Height);

        this.layer = layer;
    }

    public void ChangeSprite(string imagePath)
    {
        sprite = new Sprite2d(uiManager.contentManager, uiManager.graphicsDevice, imagePath, position);

        sprite.scale = new Vector2(size.X / sprite.texture2d.Bounds.Width, size.Y / sprite.texture2d.Bounds.Height);
    }

    public override void Update()
    {
        base.Update();

        sprite.position = position;
    }

    public override void Draw(SpriteBatch uiSpriteBatch)
    {
        sprite.Draw(uiSpriteBatch, layer);
    }
}
