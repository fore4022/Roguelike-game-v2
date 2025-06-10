using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StatSelection))]
public class StatUpgrade_UI : UserInterface
{
    [SerializeField]
    private Transform statElement_parent;

    public List<FileReference> files;
    public GameObject background;

    private StatSelection statSelection;

    private const float duration = 0.2f;

    private bool toggle = false;

    public override void SetUserInterface()
    {
        statSelection = GetComponent<StatSelection>();

        statSelection.Set(Managers.UserData.data.Stat);

        for(int i = 0; i < statElement_parent.childCount; i++)
        {
            statElement_parent.GetChild(i).GetComponent<StatElementUpgrade_UI>().Set(files[i]);
        }

        background.SetActive(false);
    }
    public void ToggleUI()
    {
        toggle = !toggle;

        background.SetActive(toggle);
        
        if(toggle)
        {
            transform.SetPosition(new(0, 35), duration, Ease.OutSine);
        }
        else
        {
            Managers.UserData.Save();
            transform.SetPosition(new(0, -1500), duration, Ease.OutSine);
        }
    }
}