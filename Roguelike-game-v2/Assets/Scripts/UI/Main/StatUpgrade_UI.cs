using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Stat_Manage))]
public class StatUpgrade_UI : UserInterface
{
    [SerializeField]
    private Transform statElement_parent;
    [SerializeField]
    private AudioClip increaseSound;
    [SerializeField]
    private AudioClip decreaseSound;
    [SerializeField]
    private AudioClip actionUnavailableSound;

    public List<FileReference> files;
    public TextMeshProUGUI statPointText;
    public GameObject background;

    private Stat_Manage statSelection;

    private const float duration = 0.2f;

    private bool toggle = false;

    public AudioClip IncreaseSound { get { return increaseSound; } }
    public AudioClip DecreaseSound { get { return decreaseSound; } }
    public AudioClip ActionUnavailableSound { get { return actionUnavailableSound; } }
    public override void SetUserInterface()
    {
        statSelection = GetComponent<Stat_Manage>();
        statPointText = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        statSelection.Set(Managers.Data.data.Stat);

        for(int i = 0; i < statElement_parent.childCount; i++)
        {
            statElement_parent.GetChild(i).GetComponent<StatElementUpgrade_UI>().Set(files[i]);

            files[i].SetAction += statSelection.Save;
        }

        background.SetActive(false);
    }
    public void TextUpdate()
    {
        statPointText.text = $"Stat Point : {Managers.Data.data.StatPoint}";
    }
    public void ToggleUI()
    {
        toggle = !toggle;

        background.SetActive(toggle);
        
        if(toggle)
        {
            transform.SetPosition(new(0, 40), duration, EaseType.OutSine);
        }
        else
        {
            transform.SetPosition(new(0, -1125), duration, EaseType.OutSine);
        }
    }
}