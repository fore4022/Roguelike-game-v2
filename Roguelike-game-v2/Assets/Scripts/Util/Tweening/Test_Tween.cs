using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Test_Tween : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;

    private Image image;
    private GridLayoutGroup grid;
    private Rigidbody rigid;

    private void Start()
    {
        StartCoroutine(Test_A());
    }
    private IEnumerator Test_A()
    {
        transform.SetPosition(new(0, 5), 2, Ease.InBounce).SetScale(3, 2, Ease.InBounce);
        transform.SetPosition(new(0, 2), 2, 2.5f, Ease.InBounce);

        yield return new WaitForSeconds(1);

        transform.SetRotation(new(0, 0, 90), 2, Ease.InExpo, TweenOperation.Append);
    }
}