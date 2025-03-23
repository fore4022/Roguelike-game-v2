using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UserLevelUp_UI : UserInterface, IPointerClickHandler
{
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private TextMeshProUGUI levelLog;

    private WaitForSeconds delay = new(0.5f);

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
    public override void SetUserInterface()
    {
        gameObject.SetActive(false);
    }
    public void PlayEffect(int levelUpCount)
    {
        StartCoroutine(LevelUpEffecting(levelUpCount));
    }
    private IEnumerator LevelUpEffecting(int levelUpCount)
    {
        string str = $"Lv. {Managers.UserData.data.Level - levelUpCount}";

        levelLog.text = str;

        yield return delay;

        str = $"Lv. {Managers.UserData.data.Level}";

        StartCoroutine(TextManipulator.TypeEffecting(levelLog, " -> " + str));

        yield return delay;

        StartCoroutine(TextManipulator.EraseEffecting(levelLog, str.Length));
    }
}