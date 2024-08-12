using UnityEngine.InputSystem;
public interface IMovable
{
    public void StartMove(InputAction.CallbackContext inputAction);
    public void OnMove(InputAction.CallbackContext inputAction);
    public void CancelMove(InputAction.CallbackContext inputAction);

}