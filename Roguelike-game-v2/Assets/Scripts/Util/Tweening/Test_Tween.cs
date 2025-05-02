using System.Collections;
using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;

    private void Start()
    {
        StartCoroutine(Test());
    }
    private IEnumerator Test()
    {
        transform.SetRotation(new(0, 0, 90), 2, Ease.InExpo);

        yield return new WaitForSeconds(2.5f);

        transform.SetPosition(new(0, 5), 2, Ease.InBounce);
    }
}