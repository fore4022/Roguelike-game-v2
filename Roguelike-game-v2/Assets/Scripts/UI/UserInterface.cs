using UnityEngine;
public class UserInterface : MonoBehaviour
{
    protected virtual void Awake()
    {
        Managers.UI.AddUI(this);
    }
}