using System.Collections;
using UnityEngine;
public class PauseMenu_UI : UserInterface
{
    public override void SetUserInterface()
    {
        StartCoroutine(Disable());
    }
    private IEnumerator Disable()
    {
        yield return new WaitUntil(() => Managers.UI.GetUI<Resume_UI>().IsInitalized == true);

        //yield return new WaitUntil(() => Managers.UI.GetUI<Setting_UI>().IsInitalized == true);

        yield return new WaitUntil(() => Managers.UI.GetUI<Information_UI>().IsInitalized == true);

        //yield return new WaitUntil(() => Managers.UI.GetUI<Quit_UI>().IsInitalized == true);

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}