using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    private void Start()
    {
        transform.SetRotation(new Vector3(0, 0, 90), 3);
    }
}