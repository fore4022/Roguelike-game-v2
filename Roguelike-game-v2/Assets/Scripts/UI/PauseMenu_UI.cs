using System.Collections;
using UnityEngine;
public class PauseMenu_UI : UserInterface
{
    public override void SetUI()
    {
        StartCoroutine(Disable());
    }
    private IEnumerator Disable()
    {
        yield return new WaitUntil(() => Managers.UI.GetUI<Resume_UI>().gameObject.activeSelf == false);

        //yield return new WaitUntil(() => Managers.UI.GetUI<Setting_UI>().gameObject.activeSelf == false);

        //yield return new WaitUntil(() => Managers.UI.GetUI<Information_UI>().gameObject.activeSelf == false);

        //yield return new WaitUntil(() => Managers.UI.GetUI<Quit_UI>().gameObject.activeSelf == false);

        Managers.UI.HideUI<PauseMenu_UI>();
    }
}