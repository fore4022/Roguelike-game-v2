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
    private Image skill;

    private const float limitTime = 1.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    protected override void Awake()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        base.Awake();

        animator = GetComponentInChildren<Animator>();
        background = Util.GetComponentInChildren<Image>(transform);
        skill = animator.gameObject.GetComponent<Image>();
    }
    private void Start()
    {
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(skill, minAlpha));
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(background, maxAlpha, limitTime, false));

        Debug.Log(background.color.a);

        yield return new WaitForSecondsRealtime(limitTime);

        Coroutine playAnimation = StartCoroutine(PlayAnimation());

        Managers.Scene.SetScene();

        yield return new WaitUntil(() => Time.timeScale == 1);

        Debug.Log(background.color.a);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(skill, minAlpha, limitTime));
        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(background, minAlpha, limitTime, false));

        yield return new WaitForSeconds(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSecondsRealtime(limitTime);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(skill, maxAlpha, limitTime));

        foreach (AnimatorController controller in animatorController)
        {
            animator.runtimeAnimatorController = controller;

            animator.Play(0, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }
    }
}