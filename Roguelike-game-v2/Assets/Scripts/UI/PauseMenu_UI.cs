using UnityEngine;
public class PauseMenu_UI : UserInterface
{
    private bool attackSelectionActive = false;

    public override void SetUserInterface()
    {
        Managers.UI.HideUI<PauseMenu_UI>();
    }
    protected override void Enable()
    {
        attackSelectionActive = Managers.UI.GetUI<SkillSelection_UI>().gameObject.activeSelf;

        if(attackSelectionActive)
        {
            Managers.UI.GetUI<SkillSelection_UI>().SkillOptionToggle(false);
        }

        Managers.UI.HideUI<HeadUpDisplay_UI>();
    }
    private void OnDisable()
    {
        if(attackSelectionActive)
        {
            Managers.UI.GetUI<SkillSelection_UI>().SkillOptionToggle(true);
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}