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
    private IEnumerator Loading()
    {
        if(!isInitalized)
        {
            Managers.UI.InitUI();

            yield return new WaitUntil(() => Managers.UI.isInitalized);
        }

        Managers.UI.uiElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => sceneName == Managers.Scene.CurrentScene);

        yield return new WaitUntil(() => isLoading == false);

        StartCoroutine(PlayingAnimation());

        Managers.UI.uiElementUtility.SetImageAlpha(background, minAlpha, limitTime);

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayingAnimation()
    {
        Managers.UI.uiElementUtility.SetImageAlpha(animationImage, maxAlpha, limitTime);

        while (true)
        {
            foreach (AnimatorController controller in animatorController)
            {
                animator.runtimeAnimatorController = controller;

                animator.Play(0, 0);

                yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 1.0f);
            }
        }
    }
}