using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.src.Engine.UI;
public class GridLayout : Panel
{
    private int elementsPerRow;
    private float spacing;

    public GridLayout(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, Vector2 size, int elementsPerRow, float spacing, float layer = 2) : base(identifier, uiManager, anchorPoint, offset, size, "none", layer)
    {
        this.elementsPerRow = elementsPerRow;
        this.spacing = spacing;
    }

    public override void Update()
    {
        base.Update();

        int currentRow = 0;
        int currentColumn = 0;

        foreach (UIElement child in children)
        {

            child.offset = new Vector2((currentColumn % elementsPerRow) * spacing, currentRow * spacing);

            if (currentColumn + 1 == elementsPerRow)
            {
                currentRow++;
                currentColumn = 0;

                continue;
            }

            currentColumn++;
        }

        
    }

    public override void Draw(SpriteBatch uiSpriteBatch)
    {
        base.Draw(uiSpriteBatch);
    }
}
