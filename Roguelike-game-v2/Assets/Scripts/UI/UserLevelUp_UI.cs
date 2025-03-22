using System.Collections;
using TMPro;
using UnityEngine;
public class UserLevelUp_UI : UserInterface
{
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private TextMeshProUGUI levelLog;

    protected override void Enable()
    {
        StartCoroutine(LevelUpEffecting());
    }
    public override void SetUserInterface()
    {
        gameObject.SetActive(false);
    }
    private IEnumerator LevelUpEffecting()
    {
        yield return new WaitForSeconds(1);

        string str = $"Lv. {Managers.UserData.data.Level}";

        StartCoroutine(TextManipulator.TypeEffecting(levelLog, $"Lv. {Managers.UserData.data.Level - 1}", " -> " + str));

        yield return new WaitForSeconds(1);

        StartCoroutine(TextManipulator.EraseEffecting(levelLog, str.Length));

        //
    }
}