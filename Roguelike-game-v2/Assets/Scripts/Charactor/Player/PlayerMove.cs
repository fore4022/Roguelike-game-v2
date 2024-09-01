using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMovable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Vector3 touchPosition;
    private Vector3 direction;

    public Vector3 Direction { get { return direction; } }
    public void StartMove()
    {
        touchPosition = GetVector();
        //controller enable
    }
    public void OnMove()
    {
        direction = Calculate.GetDirection(touchPosition, GetVector());

        Managers.Game.player.gameObject.transform.position += direction * Managers.Game.player.Stat.moveSpeed * Time.deltaTime;
    }
    public void CancelMove()
    {
        //controller disable
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
    private Vector3 GetVector()
    {
        return context.ReadValue<Vector3>();
    }
}