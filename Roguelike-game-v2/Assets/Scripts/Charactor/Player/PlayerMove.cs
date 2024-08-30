using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMovable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Vector2 touchPosition;

    public void StartMove()
    {
        touchPosition = GetVector2();
    }
    public void OnMove()
    {

    }
    public void CancelMove()
    {

    }
    public void Init()
    {
        touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Enable();

        touchControl.Touch.TouchPosition.started += (ctx =>
        {
            context = ctx;

            StartMove();
        });
        touchControl.Touch.TouchPosition.performed += (ctx =>
        {
            context = ctx;

            OnMove();
        });
        touchControl.Touch.TouchPosition.canceled += (ctx =>
        {
            context = ctx;

            CancelMove();
        });
    }
    private Vector2 GetVector2()
    {
        return context.ReadValue<Vector2>();
    }
}