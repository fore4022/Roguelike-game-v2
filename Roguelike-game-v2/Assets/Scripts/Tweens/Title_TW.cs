using System.Collections;
using UnityEngine;
public class Title_TW : MonoBehaviour
{
    [SerializeField]
    private Transform _dino;
    [SerializeField]
    private Transform _snake;
    [SerializeField]
    private Transform _slimeSquare;
    [SerializeField]
    private Transform _sword;
    [SerializeField]
    private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        _dino.SetScale(17, 1.2f, 0.15f, Ease.OutExpo)
            .SetPosition(new(0, 0.5f), 0.5f, 0.15f, Ease.OutCubic)
            .SetRotation(new(0, 0, 7.5f), 1, 0.15f, Ease.OutCirc)
            .SetPosition(new(0, 0), 2, Ease.InBack, TweenOperation.Append);
    }
    private IEnumerator ReOrder()
    {
        Animator anime = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = 0;
    }
}