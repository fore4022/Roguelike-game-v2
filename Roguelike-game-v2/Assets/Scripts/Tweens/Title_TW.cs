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

        // Dino
        tweens[0].SetScale(17, 1.2f, 0.15f, Ease.OutExpo)
            .SetPosition(new(0.045f, 0.515f), 0.5f, 0.15f, Ease.OutCirc)
            .SetRotation(new(0, 0, 727.5f), 1.1f, 0.15f, Ease.OutExpo);

        // Moth
        tweens[1].SetScale(3, 0.6f, 0.2f, Ease.OutExpo)
            .SetPosition(new(0.45f, -3.15f), 0.5f, 0.25f, Ease.OutSine)
            .SetRotation(new(0, 0, 20), 1f, 0.25f, Ease.OutExpo);

        // Cloud
        tweens[2].SetScale(2.5f, 0.7f, 0.3f, Ease.OutExpo)
            .SetPosition(new(2.2f, -4.05f), 0.65f, 0.3f, Ease.OutCirc)
            .SetRotation(new(0, 0, 7.5f), 1f, 0.3f, Ease.OutExpo);

        // SlimeSquare
        tweens[3].SetScale(3, 0.9f, 0.4f, Ease.OutExpo)
            .SetPosition(new(-2.9f, -0.8f), 1f, 0.4f, Ease.OutBack)
            .SetRotation(new(0, 0, 1120), 1.1f, 0.4f, Ease.OutExpo);

        // BatSmallA
        tweens[4].SetScale(2.75f, 0.9f, 0.45f, Ease.OutExpo)
            .SetPosition(new(-3.5f, 0.6f), 1f, 0.425f, Ease.OutBack)
            .SetRotation(new(0, 0, 740), 1.1f, 0.425f, Ease.OutExpo);

        // Mushroom_1
        tweens[5].SetScale(2, 1.05f, 0.4f, Ease.OutExpo)
            .SetPosition(new(3.5f, 6.1f), 1.15f, 0.45f, Ease.OutExpo)
            .SetRotation(new(0, 0, 390), 1.25f, 0.475f, Ease.OutExpo);

        // Mushroom_2
        tweens[6].SetScale(3, 1.2f, 0.45f, Ease.OutExpo)
            .SetPosition(new(3f, -1.4f), 0.8f, 0.45f, Ease.OutQuart)
            .SetRotation(new(0, 0, 150), 1.2f, 0.45f, Ease.OutQuart);

        // PotionI
        tweens[7].SetScale(3, 1.2f, 0.55f, Ease.OutExpo)
            .SetPosition(new(-3f, 6.75f), 0.8f, 0.45f, Ease.OutQuart)
            .SetRotation(new(0, 0, 405f), 1.15f, 0.475f, Ease.OutQuad);

        // Sword
        tweens[8].SetScale(4, 1.2f, 0.45f, Ease.OutExpo)
            .SetPosition(new(-1.7f, 6.3f), 0.8f, 0.45f, Ease.OutExpo)
            .SetRotation(new(0, 0, 752.5f), 1.2f, 0.45f, Ease.OutExpo);

        // Mask
        tweens[9].SetScale(3.2f, 1.2f, 0.45f, Ease.OutQuint)
            .SetPosition(new(3f, -7f), 0.8f, 0.45f, Ease.OutQuint)
            .SetRotation(new(0, 0, 15), 1.2f, 0.5f, Ease.OutQuint);
    }
    private IEnumerator ReOrder()
    {
        Animator anime = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = -1;

        StartCoroutine(Managers.UI.GetUI<TitlePrompt_UI>().UserDataLoading());
    }
}