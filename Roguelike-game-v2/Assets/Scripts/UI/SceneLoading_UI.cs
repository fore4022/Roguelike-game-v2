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
    private Image image;

    private const float limitTime = 1.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    protected override void Awake()
    {
        transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        base.Awake();

        animator = GetComponentInChildren<Animator>();
        image = Util.GetComponentInChildren<Image>(transform);
    }
    private void Start()
    {
        StartCoroutine(Loading());
        StartCoroutine(PlayAnimation());
    }
    private IEnumerator Loading()
    {
        animator.gameObject.SetActive(false);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, limitTime));

        yield return new WaitForSecondsRealtime(limitTime);
        
        Managers.Scene.SetScene();

        yield return new WaitUntil(() => Time.timeScale == 1);

        yield return new WaitForSeconds(0.5f);

        animator.gameObject.SetActive(false);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, limitTime));

        yield return new WaitForSeconds(limitTime);

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSecondsRealtime(limitTime * 2);

        foreach (AnimatorController controller in animatorController)
        {
            animator.runtimeAnimatorController = controller;

            animator.Play(0, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }
    }
}