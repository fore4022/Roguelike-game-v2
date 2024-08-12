using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : IMovable
{
    public void StartMove(InputAction.CallbackContext inputAction)
    {
        Vector2 position = inputAction.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext inputAction)
    {
        Vector2 position = inputAction.ReadValue<Vector2>();
    }
    public void CancelMove(InputAction.CallbackContext inputAction)
    {
        Vector2 position = inputAction.ReadValue<Vector2>();
    }
}