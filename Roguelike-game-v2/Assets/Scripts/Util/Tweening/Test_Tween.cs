using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    private void Start()
    {
        transform.SetScale(2, 2, Ease.InOutExpo);
    }
}