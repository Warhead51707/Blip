using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.Sprite;
public class Sprite2d
{
    public Vector2 position;
    public Vector2 scale = new Vector2(1f, 1f);
    public float rotation = 0f;
    public Color color = Color.White;
    public Rectangle spriteSheetPosition;

    public Texture2D texture2d;

    private ContentManager contentManager;
    private GraphicsDevice graphicsDevice;

    public Sprite2d(ContentManager contentManager, GraphicsDevice graphicsDevice, string spritePath, Vector2 position)
    {
        this.contentManager = contentManager;
        this.graphicsDevice = graphicsDevice;

        texture2d = SpriteHelper.createTextureFromPNG(spritePath, graphicsDevice);

        this.position = position;

        spriteSheetPosition = new Rectangle(0, 0, texture2d.Bounds.Width, texture2d.Bounds.Height);
    }

    public void Draw(SpriteBatch levelSpriteBatch, float layer = 1f)
    {
        levelSpriteBatch.Draw(texture2d, position, spriteSheetPosition, color, rotation, new Vector2(texture2d.Bounds.Width / 2, texture2d.Bounds.Height / 2), scale, SpriteEffects.None, layer * 0.001f);
    }
}
