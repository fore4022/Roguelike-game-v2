using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : UserInterface
{
    [SerializeField]
    private GameObject stick;

    private InputAction.CallbackContext? touchStart;
    private Vector2 enterPosition;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        TouchControls touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Touch.TouchPosition.performed += (ctx => SetJoyStick(ctx));

        touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            touchStart = null;
        });
    }
    private void SetJoyStick(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

        if(touchStart == null)
        {
            touchStart = context;

            enterPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

            transform.position = Camera.main.WorldToScreenPoint(enterPosition);
        }

        Vector2 position = enterPosition + Vector2.ClampMagnitude(touchPosition - enterPosition, Mathf.Sqrt(0.5f));

        stick.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}