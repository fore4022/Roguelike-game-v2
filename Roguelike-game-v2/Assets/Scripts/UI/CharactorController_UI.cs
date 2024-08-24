using UnityEngine;
using UnityEngine.InputSystem;
public class CharactorController_UI : MonoBehaviour
{
    private IInputActionCollection2 touchControls;

    public void Start()
    {
        touchControls = new TouchControls();
    }
}
