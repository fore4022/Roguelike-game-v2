using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillOption_UI : Button_2
{
    private List<TextMeshProUGUI> textList = new();
    private AttackContext info = null;
    private Image image;
    private RectTransform imageRect;

    protected override void PointerClick()
    {
        Managers.Game.inGameData.attack.SetValue(info.data.type);
        Managers.UI.GetUI<SkillSelection_UI>().Selected();
    }
    protected override void Init()
    {
        base.Init();

        image = Util.GetComponentInChildren<Image>(transform);
        textList = Util.GetComponentsInChildren<TextMeshProUGUI>(transform);
        imageRect = image.gameObject.GetComponent<RectTransform>();
    }
    public void Reset()
    {
        info = null;
    }
    public void InitOption(AttackContext info)
    {
        this.info = info;

        UIElementUtility.SetImageScale(rectTransform, minScale);

        SetOption();
    }
    private void SetOption()
    {
        Vector2 size;

        image.sprite = info.data.icon;
        size = image.sprite.bounds.size;

        if(size.x > size.y)
        {
            imageRect.localScale = new Vector3(1, 1 * (size.y / size.x));
        }
        else if(size.y > size.x)
        {
            imageRect.localScale = new Vector3(1 * (size.x / size.y), 1);
        }
        else
        {
            imageRect.localScale = Calculate.GetVector(1);
        }
        
        textList[0].text = $"{info.data.type}";
        textList[1].text = $"{info.data.explanation}";

        if(info.caster == null)
        {
            textList[2].text = "New";
        }
        else
        {
            textList[2].text = $"Lv. {info.level + 1}";
        }
    }
}