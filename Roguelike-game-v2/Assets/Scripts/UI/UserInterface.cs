using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    protected bool isInitalized = false;

    protected virtual void Awake()
    {
        Managers.UI.AddUI(this);
    }
    protected virtual void OnEnable()//
    {
        if(!isInitalized)
        {
            return;
        }
    }
    public abstract void SetUI();
}