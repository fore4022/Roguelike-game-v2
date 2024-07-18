using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    private TouchControls touchControls;

    private void Awake()
    {
        touchControls = new();

        touchControls.Touch.Enable();
    }
    private void Start()
    {
        touchControls.Touch.TouchPosition.started += ctx => Move();
        touchControls.Touch.TouchPosition.performed += ctx => Move();
        touchControls.Touch.TouchPosition.canceled += ctx => Move();
    }
    private void Move()
    {
        obj.SetActive(true);
        Debug.Log("asdf");
    }
}
