using System.Collections;
using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Test());
    }
    private IEnumerator Test()
    {
        transform.SetScale(2, 2);

        yield return new WaitForSeconds(1);

        transform.StopTween();

        yield return new WaitForSeconds(1);

        transform.PlayTween();
    }
}