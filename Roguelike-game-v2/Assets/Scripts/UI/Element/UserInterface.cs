using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    public bool IsInitalized { get { return isInitalized; } }
    public void SetUI()
    {
        if(!isInitalized)
        {
            isInitalized = true;

            SetUserInterface();
        }
    }
    protected void OnEnable()
    {
        if(!isInitalized)
        {
            Managers.UI.Add(this);
        }
        else
        {
            Enable();
        }
    }
    protected virtual void Start()
    {
        SetUI();
    }
    protected virtual void Enable() { }
    public abstract void SetUserInterface();
}