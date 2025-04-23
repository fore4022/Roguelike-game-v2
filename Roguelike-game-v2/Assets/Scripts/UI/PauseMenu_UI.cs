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
        attackSelectionActive = Managers.UI.GetUI<AttackSelection_UI>().gameObject.activeSelf;

        if(attackSelectionActive)
        {
            Managers.UI.GetUI<AttackSelection_UI>().AttackOptionToggle(false);
        }

        Managers.UI.HideUI<HeadUpDisplay_UI>();
    }
    private void OnDisable()
    {
        if(attackSelectionActive)
        {
            Managers.UI.GetUI<AttackSelection_UI>().AttackOptionToggle(true);
        }
        else
        {
            Time.timeScale = 1;
        }

        Managers.UI.ShowUI<HeadUpDisplay_UI>();
    }
}