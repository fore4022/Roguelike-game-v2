using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public bool IsInitalized { get { return isInitalized; } }
    protected void OnEnable()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
            Managers.UI.AddUI(this);

            return;
        }
        else
        {
            Enable();
        }
    }
    protected virtual void Enable() { }
    public abstract void SetUserInterface();
}