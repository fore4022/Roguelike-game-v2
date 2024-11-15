using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    public bool isInitalized = false;

    protected virtual void OnEnable()
    {
        if(!isInitalized)
        {
            Managers.UI.AddUI(this);

            return;
        }
    }
    public abstract void SetUI();
}