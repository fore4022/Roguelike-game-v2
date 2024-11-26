using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : UserInterface
{
    [SerializeField]
    private GameObject stick;

    private InputAction.CallbackContext? touchStart;
    private Vector2 enterPosition;
    private Vector2 touchPosition;
    private Vector2 position;

    private const int maxLength = 100;

    public override void SetUserInterface()
    {
        TouchControls touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Touch.TouchPosition.performed += (ctx => SetJoyStick(ctx));

        touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            touchStart = null;
        });

        Managers.UI.HideUI<CharactorController_UI>();
    }
    private void SetJoyStick(InputAction.CallbackContext context)
    {
        if (touchStart == null)
        {
            touchStart = context;

            enterPosition = context.ReadValue<Vector2>();

            transform.position = enterPosition;
        }

        touchPosition = context.ReadValue<Vector2>();

        position = enterPosition + Vector2.ClampMagnitude(touchPosition - enterPosition, maxLength);

        stick.transform.position = position;
    }
}