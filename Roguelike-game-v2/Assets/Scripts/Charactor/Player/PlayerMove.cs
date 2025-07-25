using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// InputSystem�� �̿��Ͽ� �����Ͽ���.
/// �Է� �̺�Ʈ�� ���� CharactorController_UI�� �����Ѵ�. 
/// </summary>
public class PlayerMove : IMoveable
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
    public bool IsPointerOverUI { set { isPointerOverUI = value; } }
    public void Init()
    {
        Util.GetMonoBehaviour().StartCoroutine(Initalization());
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
    public void SetDirection()
    {
        if(direction.x > 0)
        {
            render.flipX = false;
        }
        else if(direction.x < 0)
        {
            render.flipX = true;
        }
    }
    private void CancelMove()
    {
        Managers.UI.Hide<CharactorController_UI>();

        if(moving != null)
        {
            Util.GetMonoBehaviour().StopCoroutine(moving);
        }

        moving = null;
        active = false;
        didStartMove = false;

        Managers.Game.player.AnimationPlay("idle");
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
        moving = Util.GetMonoBehaviour().StartCoroutine(Moving());
    }
    public PlayerMove(SpriteRenderer render, DefaultMoveable moveable)
    {
        this.render = render;
        this.moveable = moveable;
    }
    private IEnumerator Moving()
    {
        didStartMove = true;

        Managers.Game.player.AnimationPlay("walk");

        while(true)
        {
            Managers.Game.player.gameObject.transform.position += direction.normalized * SpeedAmount;

            SetDirection();
            charactorController.SetJoyStick();

            yield return null;
        }
    }
}