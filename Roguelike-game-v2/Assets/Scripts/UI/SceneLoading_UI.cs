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

    private const float limitTime = 0.75f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    protected override void Awake()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        base.Awake();

        animator = GetComponentInChildren<Animator>();
        background = Util.GetComponentInChildren<Image>(transform);
    }
    private void Start()
    {
        StartCoroutine(Loading());
        StartCoroutine(PlayAnimation());
    }
    private IEnumerator Loading()
    {
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(background, maxAlpha, limitTime));

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => Time.timeScale == 1);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(background, minAlpha, limitTime));

        yield return new WaitForSecondsRealtime(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayAnimation()
    {
        foreach (AnimatorController controller in animatorController)
        {
            animator.runtimeAnimatorController = controller;

            animator.Play(0, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }
    }
}