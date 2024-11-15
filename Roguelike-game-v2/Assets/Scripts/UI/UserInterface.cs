using UnityEngine;
public abstract class UserInterface : MonoBehaviour
{
    private bool isInitalized = false;

    protected virtual void OnEnable()
    {
        if(!isInitalized)
        {
            Managers.UI.AddUI(this);

            isInitalized = true;
        }

        if (!isInitalized)
        {
            return;
        }
    }
    public abstract void SetUI();
}