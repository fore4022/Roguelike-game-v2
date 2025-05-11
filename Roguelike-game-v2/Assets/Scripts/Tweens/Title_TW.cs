using System.Collections;
using UnityEngine;
public class Title_TW : MonoBehaviour
{
    [SerializeField]
    private Transform _dino;
    [SerializeField]
    private Transform _mon1;
    [SerializeField]
    private Transform _mon2;
    [SerializeField]
    private Transform _mon3;
    [SerializeField]
    private Transform _mon4;
    [SerializeField]
    private Transform _mon5;
    [SerializeField]
    private Transform _mon6;
    [SerializeField]
    private Transform _mon7;
    [SerializeField]
    private Transform _mon8;
    [SerializeField]
    private Transform _mon9;
    [SerializeField]
    private Transform _mon10;
    [SerializeField]
    private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        // dino
        _dino.SetScale(17, 1.2f, 0.15f, Ease.OutExpo)
            .SetPosition(new(0, 0.5f), 0.5f, 0.15f, Ease.OutCirc)
            .SetRotation(new(0, 0, 727.5f), 1f, 0.15f, Ease.OutExpo);

        // mon1
        _mon1.SetScale(3, 0.6f, 0.2f, Ease.OutExpo)
            .SetPosition(new(0.45f, -3.15f), 0.65f, 0.25f, Ease.OutCirc)
            .SetRotation(new(0, 0, 20), 1f, 0.25f, Ease.OutExpo);

        // mon2
        _mon2.SetScale(2.5f, 0.7f, 0.3f, Ease.OutExpo)
            .SetPosition(new(2.2f, -4.05f), 0.65f, 0.3f, Ease.OutCirc)
            .SetRotation(new(0, 0, 7.5f), 1f, 0.3f, Ease.OutExpo);

        // mon3
        _mon3.SetScale(3, 0.9f, 0.4f, Ease.OutExpo)
            .SetPosition(new(-2.9f, -0.8f), 1f, 0.4f, Ease.OutCirc)
            .SetRotation(new(0, 0, 40), 1.1f, 0.4f, Ease.OutExpo);

        // mon4

        // mon5

        // mon6

        // mon7

        // mon8

        // mon9

        // mon10
    }
    private IEnumerator ReOrder()
    {
        Animator anime = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = -1;
    }
}