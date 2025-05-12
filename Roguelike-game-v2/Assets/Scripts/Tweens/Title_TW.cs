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
    private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        // Dino
        _dino.SetScale(17, 1.2f, 0.15f, Ease.OutExpo)
            .SetPosition(new(0, 0.5f), 0.5f, 0.15f, Ease.OutCirc)
            .SetRotation(new(0, 0, 727.5f), 1.1f, 0.15f, Ease.OutExpo);

        // Moth
        _mon1.SetScale(3, 0.6f, 0.2f, Ease.OutExpo)
            .SetPosition(new(0.45f, -3.15f), 0.65f, 0.25f, Ease.InCubic)
            .SetRotation(new(0, 0, 20), 1f, 0.25f, Ease.OutExpo);

        // Cloud
        _mon2.SetScale(2.5f, 0.7f, 0.3f, Ease.OutExpo)
            .SetPosition(new(2.2f, -4.05f), 0.65f, 0.3f, Ease.OutCirc)
            .SetRotation(new(0, 0, 7.5f), 1f, 0.3f, Ease.OutExpo);

        // SlimeSquare
        _mon3.SetScale(3, 0.9f, 0.4f, Ease.OutExpo)
            .SetPosition(new(-2.9f, -0.8f), 1f, 0.4f, Ease.OutBack)
            .SetRotation(new(0, 0, 1120), 1.1f, 0.4f, Ease.OutExpo);

        // Mushroom_1
        _mon4.SetScale(2, 1.05f, 0.4f, Ease.OutExpo)
            .SetPosition(new(3f, 5.5f), 1f, 0.4f, Ease.OutExpo)
            .SetRotation(new(0, 0, 390), 1.1f, 0.4f, Ease.OutExpo);

        // Mushroom_2
        _mon5.SetScale(3, 1.2f, 0.45f, Ease.OutExpo)
            .SetPosition(new(3f, -1.4f), 0.8f, 0.45f, Ease.OutQuart)
            .SetRotation(new(0, 0, 150), 1.2f, 0.45f, Ease.OutQuart);

        // Sword_1
        _mon6.SetScale(3, 1.2f, 0.55f, Ease.OutExpo)
            .SetPosition(new(-2.25f, 6.5f), 0.8f, 0.55f, Ease.OutQuad)
            .SetRotation(new(0, 0, 332.5f), 1.2f, 0.55f, Ease.OutQuad);

        // Sword_2
        _mon7.SetScale(4, 1.2f, 0.45f, Ease.OutExpo)
            .SetPosition(new(-1.85f, 5f), 0.8f, 0.45f, Ease.OutExpo)
            .SetRotation(new(0, 0, 752.5f), 1.2f, 0.45f, Ease.OutExpo);

        // Mask
        _mon8.SetScale(1.8f, 1.2f, 0.45f, Ease.OutQuint)
            .SetPosition(new(3f, -7f), 0.8f, 0.45f, Ease.OutQuint)
            .SetRotation(new(0, 0, 15), 1.2f, 0.45f, Ease.OutQuint);
    }
    private IEnumerator ReOrder()
    {
        Animator anime = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = -1;
    }
}