using System.Collections;
using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;

    private void Start()
    {
        StartCoroutine(Test_A());
        StartCoroutine(Test_B());
    }
    private IEnumerator Test_A()
    {
        transform.SetRotation(new(0, 0, 90), 2, Ease.InExpo);

        yield return new WaitForSeconds(2.5f);

        transform.SetPosition(new(0, 5), 2, Ease.InBounce);
    }
    private IEnumerator Test_B()
    {
        yield return new WaitForSeconds(1.5f);

        transform.KillTween();
    }
}