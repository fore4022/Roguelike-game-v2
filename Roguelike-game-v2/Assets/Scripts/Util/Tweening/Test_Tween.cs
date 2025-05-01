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
        //transform.SetScale(2, 2);

        //yield return new WaitForSeconds(1);

        //transform.StopTween();

        //yield return new WaitForSeconds(1);

        //transform.PlayTween();

        transform.SetRotation(new(0, 0, 90), 2);

        yield return new WaitForSeconds(2.5f);

        transform.SetRotation(new(0, 0, 90), 2);
    }
}