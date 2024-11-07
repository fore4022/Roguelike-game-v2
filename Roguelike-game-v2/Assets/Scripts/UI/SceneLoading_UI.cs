using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{
    private List<AnimationClip> animationClips = new();

    public bool isLoading = true;

    private Animator animator;
    private Image image;

    private const float limitTime = 0.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    protected override void Awake()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        base.Awake();

        animator = GetComponentInChildren<Animator>();
        image = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, limitTime));

        yield return new WaitForSeconds(limitTime);

        while(isLoading)
        {
            //animation play

            foreach(AnimationClip clip in animationClips)
            {

            }

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, limitTime));

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
}