using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillOption_UI : Button_B
{
    private List<TextMeshProUGUI> textList = new();
    private SkillContext info = null;
    private Image image;
    private RectTransform imageRect;

    protected override void PointerClick()
    {
        Managers.Game.inGameData.skill.SetValue(info.data.type);
        Managers.UI.Get<SkillSelection_UI>().Selected();
    }
    protected override void Init()
    {
        base.Init();

        image = Util.GetComponentInChildren<Image>(transform);
        textList = Util.GetComponentsInChildren<TextMeshProUGUI>(transform);
        imageRect = image.gameObject.GetComponent<RectTransform>();

        Managers.Audio.Registration(audioSource);
    }
    public void Reset()
    {
        info = null;
    }
    public void InitOption(SkillContext info)
    {
        this.info = info;

        transform.SetScale(minScale);
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
            imageRect.localScale = new Vector2(1, 1);
        }
        
        textList[0].text = $"{info.data.name}";

        if(info.caster == null)
        {
            textList[1].text = "New";
        }
        else
        {
            textList[1].text = $"Lv. {info.level + 1}";
        }

        textList[2].text = $"{info.data.explanation}";
    }
}