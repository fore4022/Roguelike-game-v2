using UnityEngine;
public class UserInterface : MonoBehaviour
{
    protected virtual void Start()
    {
        Managers.UI.AddUI(this);
    }
}