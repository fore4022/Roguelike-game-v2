using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AttackInformation_SO info;

    private Image image;
    private Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    private void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }
    public void SetInformation(AttackInformation_SO info)
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
        image.sprite = null;
        animator.runtimeAnimatorController = null;
        info = null;
    }
}