using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject stick;

    private Vector2 enterTouchPosition;

    private void Start()
    {
        TouchControls touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Touch.TouchPosition.started += (ctx => SetPosition(ctx));
        touchControl.Touch.TouchPosition.performed += (ctx => SetJoyStick(ctx));
    }
    private void SetPosition(InputAction.CallbackContext context)
    {
        enterTouchPosition = context.ReadValue<Vector2>();

        transform.position = enterTouchPosition;

        enterTouchPosition = Camera.main.ScreenToWorldPoint(enterTouchPosition);
    }
    private void SetJoyStick(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        Vector2 position = enterTouchPosition + Vector2.ClampMagnitude(touchPosition - enterTouchPosition, 100);

        stick.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
