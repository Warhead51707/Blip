using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.Sprite;
public class SpriteHelper
{
    public static Texture2D createTextureFromPNG(string filePath, GraphicsDevice graphicsDevice)
    {
        FileStream fileStream = new FileStream("Content/assets/sprites/" + filePath + ".png", FileMode.Open);
        Texture2D texture2d = Texture2D.FromStream(graphicsDevice, fileStream);
        fileStream.Dispose();

        return texture2d;
    }
}
