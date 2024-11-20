using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public void SetUI()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
        }
    }
    public bool IsInitalized 
    {
        get
        {
            return isInitalized; 
        }
    }
    protected void OnEnable()
    {
        if(!isInitalized)
        {
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