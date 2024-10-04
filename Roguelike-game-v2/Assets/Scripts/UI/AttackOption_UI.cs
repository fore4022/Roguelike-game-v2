using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AttackInformation_SO info;

    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;

    private const float minScale = 200;
    private const float maxScale = 325;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAnimator(true);

        StartCoroutine(SetImageScale(maxScale));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetAnimator(false);

        StartCoroutine(SetImageScale(minScale));
    }
    private void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        SetAnimator(false);

        gameObject.SetActive(false);
    }
    public void InitOption(AttackInformation_SO info)
    {
        this.info = info;

        SetOption();

        gameObject.SetActive(true);
    }
    private void SetOption()
    {
        image.sprite = info.sprite;
        animator.runtimeAnimatorController = info.controller;
    }
    private void OnDisable()
    {
        //image.sprite = null;
        //animator.runtimeAnimatorController = null;
        info = null;
    }
    private void SetAnimator(bool isEnabled)
    {
        animator.enabled = isEnabled;
    }
    private IEnumerator SetImageScale(float targetScale)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float scale = rectTransform.localScale.x;
        float sign;

        if(targetScale > scale)
        {
            sign = maxScale - scale;
        }
        else
        {
            sign = scale - minScale;
        }

        Debug.Log((int)Mathf.Abs(targetScale - rectTransform.localScale.x) == 0);

        while((int)Mathf.Abs(targetScale - rectTransform.localScale.x) == 0)
        {
            rectTransform.localScale += new Vector3(sign, sign) * Time.deltaTime;

            Debug.Log(new Vector3(sign, sign) * Time.deltaTime);

            yield return null;
        }
    }
}