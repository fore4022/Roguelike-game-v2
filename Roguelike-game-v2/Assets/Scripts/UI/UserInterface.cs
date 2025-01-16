using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public bool IsInitalized { get { return isInitalized; } }
    protected virtual void Awake()
    {
        Managers.UI.AddUI(this);
    }
    protected void OnEnable()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
        }
        else
        {
            Enable();
        }
    }
    protected virtual void Enable() { }
    public abstract void SetUserInterface();
}