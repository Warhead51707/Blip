using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class NineSlice
{
    public Vector4 position;

    private Texture2D texture;
    private int padding;

    private Rectangle[] sourcePatches;

    private Vector4[] destinationPatches;

    public NineSlice(Texture2D texture, int padding, Vector4 destination)
    {
        this.texture = texture;
        this.padding = padding;
        this.position = destination;

        sourcePatches = CreatePatches(texture.Bounds);
    }

    private Rectangle[] CreatePatches(Rectangle rectangle)
    {
        int x = rectangle.X;
        int y = rectangle.Y;
        int width = rectangle.Width;
        int height = rectangle.Height;

        int centerWidth = width - padding * 2;
        int centerHeight = height - padding * 2;

        int bottomY = y + height - padding;

        int rightX = x + width - padding;

        int leftX = x + padding;

        int topY = y + padding;

        Rectangle[] patches = new[]
        {
            new Rectangle(x, y, padding, padding),      // top left
            new Rectangle(leftX, y, centerWidth, padding),      // top middle
            new Rectangle(rightX, y, padding, padding),      // top right
            new Rectangle(x, topY, padding, centerHeight),    // left middle
            new Rectangle(leftX, topY, centerWidth, centerHeight),    // middle
            new Rectangle(rightX, topY,padding, centerHeight),    // right middle
            new Rectangle(x, bottomY, padding, padding),   // bottom left
            new Rectangle(leftX, bottomY, centerWidth, padding),   // bottom middle
            new Rectangle(rightX, bottomY,padding, padding)    // bottom right
        };

        return patches;
    }

    private Vector4[] createDestinationPatches() 
    {
        float x = position.X;
        float y = position.Y;
        float width = position.Z;
        float height = position.W;

        float centerWidth = width - padding * 2;
        float centerHeight = height - padding * 2;

        float bottomY = y + height - padding;

        float rightX = x + width - padding;

        float leftX = x + padding;

        float topY = y + padding;

        Vector4[] patches = new[]
        {
            new Vector4(x, y, padding, padding),      // top left
            new Vector4(leftX, y, centerWidth, padding),      // top middle
            new Vector4(rightX, y, padding, padding),      // top right
            new Vector4(x, topY, padding, centerHeight),    // left middle
            new Vector4(leftX, topY, centerWidth, centerHeight),    // middle
            new Vector4(rightX, topY,padding, centerHeight),    // right middle
            new Vector4(x, bottomY, padding, padding),   // bottom left
            new Vector4(leftX, bottomY, centerWidth, padding),   // bottom middle
            new Vector4(rightX, bottomY,padding, padding)    // bottom right
        };

        return patches;
    }


    public void Draw(SpriteBatch spriteBatch, float layer = 1f)
    {

        destinationPatches = createDestinationPatches();

        for (int i = 0; i < sourcePatches.Length; i++)
        {
            Vector2 scale = new Vector2(destinationPatches[i].Z / sourcePatches[i].Width, destinationPatches[i].W / sourcePatches[i].Height);

            spriteBatch.Draw(texture, new Vector2(destinationPatches[i].X, destinationPatches[i].Y), sourcePatches[i], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layer * 0.001f);
        }
    }
}