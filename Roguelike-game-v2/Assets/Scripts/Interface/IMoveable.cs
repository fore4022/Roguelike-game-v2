using UnityEngine.InputSystem;
public interface IMoveable
{
    public void StartMove();
    public void OnMove();
    public void CancelMove();
}