using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    [HideInInspector]
    public bool isInitalized = false;

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
    public abstract void SetUI();
}