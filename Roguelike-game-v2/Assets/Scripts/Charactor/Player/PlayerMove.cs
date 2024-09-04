using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMovable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Coroutine moving;
    private Vector2 enterTouchPosition;
    private Vector2 direction;

    private string controller = "CharactorController";

    public void StartMove()
    {
        enterTouchPosition = context.ReadValue<Vector2>();
        enterTouchPosition = Camera.main.ScreenToWorldPoint(enterTouchPosition);

        Managers.UI.ShowUI(controller);
    }
    public void OnMove()
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        direction = Calculate.GetDirection(touchPosition, enterTouchPosition);

        if(moving == null)
        {
            moving = Util.GetMonoBehaviour().StartCoroutine(Moving());
        }
    }
    public void CancelMove()
    {
        direction = Vector2.zero;

        Util.GetMonoBehaviour().StopCoroutine(moving);

        moving = null;

        Managers.UI.HideUI(controller);
    }
    public void Init()
    {
        touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Enable();

        touchControl.Touch.TouchPress.started += (ctx =>
        {
            StartMove();
        });
        touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            CancelMove();
        });

        touchControl.Touch.TouchPosition.started += (ctx =>
        {
            context = ctx;
        });
        touchControl.Touch.TouchPosition.performed += (ctx =>
        {
            context = ctx;

            OnMove();
        });
    }
    private IEnumerator Moving()
    {
        while(true)
        {
            Managers.Game.player.gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * 2 * Time.deltaTime;

            yield return null;
        }
    }
}