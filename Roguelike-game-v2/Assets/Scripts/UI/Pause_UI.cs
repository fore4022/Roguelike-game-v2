using UnityEngine;
public class Pause_UI : NewButton
{
    protected override void PointerClick()
    {
        Time.timeScale = 0;

        //show UI

        Managers.UI.HideUI<Pause_UI>();
    }
    protected override void Init()
    {
        minScale = 1f;
        maxScale = 1.035f;
        minAlpha = 155;
        maxAlpha = 235;
        duration = 0.1f;
    }
    protected override void Start()
    {
        childClass = this;

        base.Start();
    }
}