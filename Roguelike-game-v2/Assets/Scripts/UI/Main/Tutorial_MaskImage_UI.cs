using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Tutorial_MaskImage_UI : UserInterface, IPointerEnterHandler
{
    [SerializeField]
    private List<Transform> targetList;
    [Header("Step_0")]
    [SerializeField]
    private TextMeshProUGUI text_0;
    [Header("Step_1")]
    [SerializeField]
    private TextMeshProUGUI text_1;
    [Header("Step_2")]
    [SerializeField]
    private TextMeshProUGUI text_2;

    private Image maskImage;

    private const string stepName = "Step_";
    private const float duration = 0.3f;
    private const float targetAlpha = 165;

    private Coroutine step = null;
    private Coroutine typing = null;
    private string targetStr =  "";
    private int targetIndex = 0;

    public override void SetUserInterface()
    {
        Managers.UI.Hide<Tutorial_MaskImage_UI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(step == null)
        {
            StartCoroutine($"{stepName}{targetIndex}");

            targetIndex++;
        }
        else
        {

        }
    }
    protected override void Enable()
    {
        targetIndex = 0;

        UIElementUtility.SetImageAlpha(maskImage, 0);

        step = StartCoroutine(Step_0());
    }
    private IEnumerator Step_0()
    {
        targetStr = "아래 플레이 버튼으로\n게임을 시작할 수 있습니다.";
        typing = StartCoroutine(Typing.TypeEffecting(text_0, targetStr));

        yield return null;

        step = null;
    }
}