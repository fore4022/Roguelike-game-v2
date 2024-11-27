using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerMove : IMoveable
{
    private TouchControls touchControl;

    private InputAction.CallbackContext context;
    private Coroutine moving;
    private Vector2? enterTouchPosition;
    private Vector2 touchPosition;
    private Vector2 direction;

    private bool isPointerOverUI;
    private bool active = false;

    public Vector2 Direction { get { return direction; } }
    public void OnMove()
    {
        touchPosition = context.ReadValue<Vector2>();

        direction = Managers.Game.calculate.GetDirection(touchPosition, (Vector2)enterTouchPosition);
    }
    private void StartMove()
    {
        enterTouchPosition = context.ReadValue<Vector2>();
        moving = Util.GetMonoBehaviour().StartCoroutine(Moving());
    }
    private void CancelMove()
    {
        Managers.UI.HideUI<CharactorController_UI>();

        if(moving != null)
        {
            Util.GetMonoBehaviour().StopCoroutine(moving);
        }

        enterTouchPosition = null;
        moving = null;

        active = false;
    }
    public void Init()
    {
        touchControl = InputActions.CreateAndGetInputAction<TouchControls>();

        touchControl.Enable();

        touchControl.Touch.TouchPress.started += (ctx =>
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("is Pointer Over UI");

                return;
            }

            active = true;

            Managers.UI.ShowUI<CharactorController_UI>();
        });

        touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            CancelMove();
        });

        touchControl.Touch.TouchPosition.performed += (ctx =>
        {
            if(!active)
            {
                return;
            }

            context = ctx;
            
            if(enterTouchPosition == null)
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
            Managers.Game.player.gameObject.transform.position += (Vector3)direction * Managers.Game.player.Stat.moveSpeed * Time.deltaTime;

            yield return null;
        }
    }
}