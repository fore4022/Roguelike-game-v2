using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMoveable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Coroutine moving;
    private Vector2? enterTouchPosition;
    private Vector2 direction;

    private string controller = "CharactorController";

    public void OnMove()
    {
        Vector2 touchPosition = context.ReadValue<Vector2>();
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        direction = Calculate.GetDirection(touchPosition, (Vector2)enterTouchPosition);
    }
    private void StartMove()
    {
        enterTouchPosition = context.ReadValue<Vector2>();
        enterTouchPosition = Camera.main.ScreenToWorldPoint((Vector2)enterTouchPosition);
        moving = Util.GetMonoBehaviour().StartCoroutine(Moving());

        Managers.UI.ShowUI(controller);
    }
    private void CancelMove()
    {
        Managers.UI.HideUI(controller);

        Util.GetMonoBehaviour().StopCoroutine(moving);

        direction = Vector2.zero;
        enterTouchPosition = null;

        moving = null;
    }
    public void Init()
    {
        touchControl = InputActions.GetInputAction<TouchControls>();

        touchControl.Enable();

        touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            CancelMove();
        });

        touchControl.Touch.TouchPosition.performed += (ctx =>//
        {
            context = ctx;
            
            if(enterTouchPosition ==null)
            {
                StartMove();
            }

            OnMove();
        });
    }
    private IEnumerator Moving()
    {
        while(true)
        {
            Managers.Game.player.gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * Managers.Game.player.DefaultStat.moveSpeed * Time.deltaTime;

            yield return null;
        }
    }
}