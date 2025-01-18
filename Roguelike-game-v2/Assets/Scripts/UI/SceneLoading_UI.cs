using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{    
    [SerializeField]
    private List<AnimatorController> animatorController = new();//

    private Animator animator;
    private Image background;
    private Image animationImage;

    private Coroutine playAnimation;
    private Coroutine SetAlpha;
    private const float limitTime = 1.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    private bool isLoading = true;

    public bool IsLoading { set { isLoading = value; } }
    public override void SetUserInterface()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        animator = GetComponentInChildren<Animator>();
        background = Util.GetComponentInChildren<Image>(transform);
        animationImage = Util.GetComponentInChildren<Image>(background.transform);

        StartCoroutine(Loading());
    }
    public void PlayAnimation()
    {
        playAnimation = StartCoroutine(PlayingAnimation());
    }
    private IEnumerator Loading()
    {
        Managers.UI.uiElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => isLoading == false);

        Managers.UI.uiElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        StopCoroutine(SetAlpha);
        StopCoroutine(playAnimation);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayingAnimation()
    {
        SetAlpha = Managers.UI.uiElementUtility.SetImageAlpha(animationImage, maxAlpha, limitTime);

        while(true)
        {
            foreach (AnimatorController controller in animatorController)
            {
                animator.runtimeAnimatorController = controller;

                animator.Play(0, 0);

                yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
        }
    }
}