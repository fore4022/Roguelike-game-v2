using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour, IMoveable
{
    private TouchControls touchControl;
    private CharactorController_UI charactorController;
    private SpriteRenderer render;

    private InputAction.CallbackContext context;
    private Coroutine moving;
    private Vector3 direction;
    private Vector2 enterTouchPosition;
    private Vector2 touchPosition;
    private bool isPointerOverUI;
    private bool active = false;
    private bool didStartMove = false;

    public Vector2 Direction { get { return direction; } }
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    public void Init()
    {
        StartCoroutine(Initalization());
    }
    public void OnMove()
    {
        touchPosition = context.ReadValue<Vector2>();
        direction = Calculate.GetDirection(touchPosition, enterTouchPosition, false);
    }
    private void CancelMove()
    {
        Managers.UI.HideUI<CharactorController_UI>();

        if(moving != null)
        {
            StopCoroutine(moving);
        }

        moving = null;
        active = false;
        didStartMove = false;

        Managers.Game.player.AnimationPlay("idle");
    }
    private void Update()
    {
        isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
    private IEnumerator Initalization()
    {
        touchControl = InputActions.CreateAndGetInputAction<TouchControls>();

        touchControl.Enable();

        yield return new WaitUntil(() => Managers.UI.GetUI<CharactorController_UI>() != null);

        charactorController = Managers.UI.GetUI<CharactorController_UI>();

        touchControl.Touch.TouchPress.started += (ctx =>
        {
            if(!isPointerOverUI)
            {
                active = true;
            }
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

            if(!didStartMove)
            {
                StartMove();
            }

            OnMove();
        });
    }
    private void StartMove()
    {
        Managers.UI.ShowUI<CharactorController_UI>();

        enterTouchPosition = context.ReadValue<Vector2>();
        charactorController.EnterPosition = enterTouchPosition;
        moving = StartCoroutine(Moving());
    }
    private IEnumerator Moving()
    {
        didStartMove = true;

        Managers.Game.player.AnimationPlay("walk");

        while(true)
        {
            Managers.Game.player.gameObject.transform.position += direction.normalized * Managers.Game.player.Stat.moveSpeed * Time.deltaTime;

            if(direction.x > 0)
            {
                render.flipX = false;
            }
            else if(direction.x < 0)
            {
                render.flipX = true;
            }

            charactorController.SetJoyStick();

            yield return null;
        }
    }
}