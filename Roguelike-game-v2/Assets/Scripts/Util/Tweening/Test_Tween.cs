using System.Collections;
using UnityEngine;
public class Test_Tween : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    private void Start()
    {
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);

        rectTransform.SetScale(1.5f, 0.75f);
    }
}