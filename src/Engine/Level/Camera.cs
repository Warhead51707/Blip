using Blip.src.Manager.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;

namespace Blip.src.Engine.Level;
public class Camera
{
    public Matrix transformMatrix = Matrix.CreateScale(5f);
    public Matrix uiTransformMatrix = Matrix.CreateScale(3f);

    private readonly int screenWidth;
    private readonly int screenHeight;

    private GameStateManager gameStateManager;

    //Click and drag movement variables
    public bool isDragging = false;
    private Vector2 initialMousePosition;

    //Zoom variables
    private MouseState previousMouseState;
    private float maxZoom = 10f;
    private float minZoom = 2f;
    private float zoomMultiplier = 0.01f;

    //Ui variables
    private bool isFocused = false;

    public Camera(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
    }

    public void addGameStateManager(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    public void Update()
    {
        if (isFocused) previousMouseState = Mouse.GetState();

        if (gameStateManager.GetGameState() == GameState.Debug && !isFocused)
        {
            Zoom();
            ClickAndDragMovement();
        }
    }

    public void Attach(Vector2 target)
    {
        Matrix position = Matrix.CreateTranslation(-target.X, -target.Y, 0) * Matrix.CreateScale(5f);
        Matrix offset = Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);

        transformMatrix = position * offset;
    }

    public void SetFocus(bool isFocused)
    {
        this.isFocused = isFocused;
    }

    public bool IsFocused()
    {
        return isFocused;
    }

    private void Zoom()
    {
        MouseState currentMouseState = Mouse.GetState();

        int deltaScrollWheelValue = currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;

        float zoomNormalizer = deltaScrollWheelValue < 0 ? 2f : 0f;

        float zoomAmount = deltaScrollWheelValue * zoomMultiplier + zoomNormalizer;

        if (deltaScrollWheelValue == 0) zoomAmount = 1;

        transformMatrix.M11 = MathHelper.Clamp(transformMatrix.M11 * zoomAmount, minZoom, maxZoom);
        transformMatrix.M22 = MathHelper.Clamp(transformMatrix.M22 * zoomAmount, minZoom, maxZoom);

        previousMouseState = currentMouseState;
    }

    private void ClickAndDragMovement()
    {
        MouseState currentMouseState = Mouse.GetState();

        if (currentMouseState.MiddleButton == ButtonState.Pressed)
        {
            if (!isDragging)
            {
                isDragging = true;
                initialMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            }

            Vector2 deltaMouse = initialMousePosition - new Vector2(currentMouseState.X, currentMouseState.Y);
            initialMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            transformMatrix *= Matrix.CreateTranslation(new Vector3(-deltaMouse.X, -deltaMouse.Y, 0));
            uiTransformMatrix *= Matrix.CreateTranslation(new Vector3(-deltaMouse.X, -deltaMouse.Y, 0));
        }
        else
        {
            isDragging = false;
        }
    }
}
