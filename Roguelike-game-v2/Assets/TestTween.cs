using UnityEngine;
public class TestTween : MonoBehaviour
{
    private void Start()
    {
        transform.SetScale(3, 1, TweenOperation.Append);
    }
}