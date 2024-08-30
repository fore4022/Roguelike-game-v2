using UnityEngine.InputSystem;
public interface IMovable
{
    public void StartMove();
    public void OnMove();
    public void CancelMove();
}