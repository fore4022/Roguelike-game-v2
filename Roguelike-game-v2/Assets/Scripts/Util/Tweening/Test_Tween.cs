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
        transform.SetPosition(new(0, 5), 2, Ease.InBounce)
            .SetScale(3, 2, Ease.InBounce)
            .SetPosition(new(0, 2), 2, Ease.InBounce, TweenOperation.Append)
            .SetRotation(new(0, 0, 90), 2, Ease.InExpo);
    }
}