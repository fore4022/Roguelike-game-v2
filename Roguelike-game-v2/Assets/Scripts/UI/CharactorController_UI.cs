using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : UserInterface
{
    [SerializeField]
    private GameObject stick;

    private InputAction.CallbackContext? touchStart;
    private Vector2 enterPosition;
    private Vector2 position;

    private const int maxLength = 100;

    public Vector2 EnterPosition
    {
        set
        {
            enterPosition = value;

            transform.position = enterPosition;
        }
    }
    public override void SetUserInterface()
    {
        Managers.UI.HideUI<CharactorController_UI>();
    }
    public void SetJoyStick()
    {
        position = enterPosition + Vector2.ClampMagnitude(Managers.Game.player.move.Direction, maxLength);

        stick.transform.position = position;
    }
}