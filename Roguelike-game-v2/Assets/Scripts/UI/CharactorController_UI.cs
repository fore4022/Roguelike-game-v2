using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : MonoBehaviour
{
    private void Start()
    {
        TouchControls touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Touch.TouchPosition.started += (ctx => SetPosition(ctx));
        touchControl.Touch.TouchPosition.performed += (ctx => SetJoyStick(ctx));
    }
    private void SetPosition(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();

        transform.position = touchPosition;
    }
    private void SetJoyStick(InputAction.CallbackContext context)
    {

    }
}
