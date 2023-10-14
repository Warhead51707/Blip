using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Web;

namespace Blip.src.Engine.UI;
public class UIElement
{
    public string identifier;
    public Vector2 position;
    public Vector2 offset;
    public Vector2 size;
    public bool visible = true;

    private Vector2 activeOffset;

    private bool isChild = false;

    private AnchorPoint anchorPoint;
    protected UIManager uiManager;

    protected UIElement(string identifier, UIManager uiManager, AnchorPoint anchorPoint, Vector2 offset, Vector2 size)
    {
        this.uiManager = uiManager;
        this.anchorPoint = anchorPoint;
        this.offset = offset;
        this.size = size;
        this.identifier = identifier;

        this.position = Vector2.Zero;

        if (anchorPoint != AnchorPoint.None) CalculateAnchorPointPosition();
    }

    public virtual void Update()
    {
        CalculateAnchorPointPosition();

        if (!isChild) position = new Vector2(-uiManager.uiCamera.uiTransformMatrix.Translation.X / uiManager.uiCamera.uiTransformMatrix.M11, -uiManager.uiCamera.uiTransformMatrix.Translation.Y / uiManager.uiCamera.uiTransformMatrix.M22) + activeOffset;

        if (anchorPoint == AnchorPoint.Mouse) position = activeOffset;
    }

    public virtual void Draw(SpriteBatch uiSpriteBatch)
    {
    }

    public void setChild(bool isChild)
    {
        this.isChild = isChild;
    }

    private void CalculateAnchorPointPosition()
    {
        if (anchorPoint == AnchorPoint.BottomLeft)
        {
            activeOffset = new Vector2(offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 3) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.MiddleLeft)
        {
            activeOffset = new Vector2(offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 5) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.TopLeft)
        {
            activeOffset = new Vector2(offset.X + size.X, offset.Y - size.Y);
        }

        if (anchorPoint == AnchorPoint.BottomMiddle)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 5) - size.X - offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 3) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.Middle)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 5) - size.X - offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 5) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.TopMiddle)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 5) - size.X - activeOffset.X, offset.Y);
        }

        if (anchorPoint == AnchorPoint.BottomRight)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3) - size.X - offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 3) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.MiddleRight)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3) - size.X - offset.X, (uiManager.graphicsAdapter.CurrentDisplayMode.Height / 5) - size.Y - offset.Y);
        }

        if (anchorPoint == AnchorPoint.TopRight)
        {
            activeOffset = new Vector2((uiManager.graphicsAdapter.CurrentDisplayMode.Width / 3) - size.X - offset.X, offset.Y);
        }

        if (anchorPoint == AnchorPoint.Mouse)
        {
            Vector2 currentMousePosition = Mouse.GetState().Position.ToVector2();

            Vector2 mouseWorldPosition = Vector2.Transform(currentMousePosition, Matrix.Invert(uiManager.uiCamera.uiTransformMatrix) * uiManager.uiCamera.uiTransformMatrix.M11);

            activeOffset = new Vector2(mouseWorldPosition.X - offset.X, mouseWorldPosition.Y - offset.Y) / 3;
        }
    }
}
