using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Title_TW : MonoBehaviour
{
    [SerializeField]
    private List<Transform> tweens;
    [SerializeField]
    private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        // Wisp
        tweens[0].SetScale(23, 1.2f, 0.1f, Ease.OutExpo)
            .SetPosition(new(-0.15f, 0.55f), 0.5f, 0.075f, Ease.OutCirc)
            .SetRotation(new(0, 0, 727.5f), 1.1f, 0.035f, Ease.OutQuint);

        // Moth
        tweens[1].SetScale(3, 0.6f, 0.15f, Ease.OutExpo)
            .SetPosition(new(-2f, -3.55f), 0.5f, 0.2f, Ease.OutSine)
            .SetRotation(new(0, 0, -20), 1f, 0.2f, Ease.OutExpo);

        // Cloud
        tweens[2].SetScale(2.5f, 0.7f, 0.25f, Ease.OutExpo)
            .SetPosition(new(2.2f, -4.05f), 0.65f, 0.25f, Ease.OutCirc)
            .SetRotation(new(0, 0, 7.5f), 1f, 0.25f, Ease.OutExpo);

        // SlimeSquare
        tweens[3].SetScale(3, 0.9f, 0.35f, Ease.OutExpo)
            .SetPosition(new(-2.9f, -0.8f), 1f, 0.35f, Ease.OutBack)
            .SetRotation(new(0, 0, 1120), 1.1f, 0.3f, Ease.OutExpo);

        // BatSmallA
        tweens[4].SetScale(2.75f, 0.9f, 0.4f, Ease.OutExpo)
            .SetPosition(new(-3.5f, 0.6f), 1f, 0.375f, Ease.OutBack)
            .SetRotation(new(0, 0, 740), 1.1f, 0.3f, Ease.OutExpo);

        // Mushroom_1
        tweens[5].SetScale(2, 1.05f, 0.3f, Ease.OutExpo)
            .SetPosition(new(3.5f, 6.1f), 0.95f, 0.35f, Ease.OutExpo)
            .SetRotation(new(0, 0, 390), 1.15f, 0.25f, Ease.OutExpo);

        // Mushroom_2
        tweens[6].SetScale(3, 1.2f, 0.35f, Ease.OutExpo)
            .SetPosition(new(2.15f, -2.55f), 0.8f, 0.35f, Ease.OutQuart)
            .SetRotation(new(0, 0, 155), 1f, 0.35f, Ease.OutQuart);

        // PotionI
        tweens[7].SetScale(3, 1.2f, 0.45f, Ease.OutExpo)
            .SetPosition(new(-3f, 6.75f), 0.8f, 0.35f, Ease.OutQuart)
            .SetRotation(new(0, 0, 405f), 1f, 0.3f, Ease.OutQuad);

        // Sword
        tweens[8].SetScale(4, 1.2f, 0.35f, Ease.OutExpo)
            .SetPosition(new(-1.7f, 6.3f), 0.8f, 0.35f, Ease.OutExpo)
            .SetRotation(new(0, 0, 752.5f), 1.1f, 0.25f, Ease.OutExpo);

        // Mask
        tweens[9].SetScale(3.2f, 1.2f, 0.35f, Ease.OutQuint)
            .SetPosition(new(3f, -7f), 0.8f, 0.35f, Ease.OutQuint)
            .SetRotation(new(0, 0, 15), 1.1f, 0.3f, Ease.OutQuint);
    }
    private IEnumerator ReOrder()
    {
        Animator Animator = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = 0;
    }
}