using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// InputSystem을 이용하여 구현하였다.
/// 입력 이벤트에 따라서 CharactorController_UI를 제어한다. 
/// </summary>
public class PlayerMove : MonoBehaviour, IMoveable
{
    private IMoveable moveable;
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
    public float SpeedAmount { get { return Managers.Game.player.Stat.moveSpeed * SlowDownAmount * Time.deltaTime; } }
    public float SlowDownAmount { get { return moveable.SlowDownAmount; } }
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        moveable = Managers.Game.container.Get<DefaultMoveable>(transform);
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
    public void SetSlowDown(float slowDown, float duration)
    {
        moveable.SetSlowDown(slowDown, duration);
    }
    private void CancelMove()
    {
        Managers.UI.Hide<CharactorController_UI>();

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

        yield return new WaitUntil(() => Managers.UI.Get<CharactorController_UI>() != null);

        charactorController = Managers.UI.Get<CharactorController_UI>();

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
        Managers.UI.Show<CharactorController_UI>();

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
            Managers.Game.player.gameObject.transform.position += direction.normalized * SpeedAmount;

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