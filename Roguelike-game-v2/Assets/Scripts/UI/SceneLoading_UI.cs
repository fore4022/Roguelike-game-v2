using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
public class SceneLoading_UI : UserInterface
{    
    [SerializeField]
    private List<AnimatorController> animatorController = new();

    private Coroutine loading;
    private Coroutine playAnimation;
    private Animator animator;
    private Image image;

    private const float limitTime = 1.5f;
    private const float minAlpha = 0;
    private const float maxAlpha = 255;

    public Coroutine LoadingCoroutine { get { return loading; } }
    public Coroutine PlayerAnimation { get { return playAnimation; } }
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
        loading = StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        animator.gameObject.SetActive(false);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, maxAlpha, limitTime));

        yield return new WaitForSeconds(limitTime);

        playAnimation = StartCoroutine(PlayAnimation());

        yield return new WaitUntil(() => Time.timeScale == 1);

        StopCoroutine(playAnimation);

        animator.gameObject.SetActive(false);

        StartCoroutine(Managers.UI.uiElementUtility.SetImageAlpha(image, minAlpha, limitTime));

        yield return new WaitForSeconds(limitTime);

        loading = null;

        Managers.UI.DestroyUI<SceneLoading_UI>();
    }
    private IEnumerator PlayAnimation()
    {
        animator.gameObject.SetActive(true);

        foreach (AnimatorController controller in animatorController)
        {
            animator.runtimeAnimatorController = controller;

            animator.Play(0, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }
    }
}