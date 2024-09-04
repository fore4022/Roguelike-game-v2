using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMovable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Vector3 touchPosition;
    private Vector3 direction;

    public void StartMove()
    {
        touchPosition = GetVector();

        Managers.UI.ShowUI("CharactorController");
    }
    public void OnMove()
    {
        direction = Calculate.GetDirection(GetVector(), touchPosition);

        Managers.Game.player.gameObject.transform.position += direction *2 * Time.deltaTime;
    }
    public void CancelMove()
    {
        Managers.UI.HideUI("CharactorController");
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
        return context.ReadValue<Vector2>();
    }
}