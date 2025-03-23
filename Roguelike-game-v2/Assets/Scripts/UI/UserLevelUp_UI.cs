using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UserLevelUp_UI : UserInterface, IPointerClickHandler
{
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private TextMeshProUGUI levelLog;
    [SerializeField]
    private TextMeshProUGUI prompt;

    private const float delaySec = 0.5f;
    private const float duration = 1.5f;
    private const int maxCount = 6;
    private const int minAlpha = 50;
    private const int maxAlpha = 255;

    private WaitForSeconds delay = new(delaySec);
    private bool allowClose = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!allowClose)
        {
            return;
        }

        for (int i = 0; i < maxCount; i++)
        {
            particles[i].SetActive(false);
        }

        gameObject.SetActive(false);
    }
    public override void SetUserInterface()
    {
        gameObject.SetActive(false);
    }
    public void OnValidate()
    {
        Util.ResizeArray(ref particles, maxCount);
    }
    public void PlayEffect(int levelUpCount)
    {
        StartCoroutine(LevelUpEffecting(levelUpCount));
    }
    private IEnumerator LevelUpEffecting(int levelUpCount)
    {
        List<Vector3> positionList = new();
        int[] indexs = Calculate.GetRandomValues(maxCount);
        string str = $"Lv. {Managers.UserData.data.Level - levelUpCount}";

        levelLog.text = str;

        foreach(int index in indexs)
        {
            positionList.Add(particles[index].transform.position);
        }

        indexs = Calculate.GetRandomValues(maxCount);
        str = $"Lv. {Managers.UserData.data.Level}";

        for(int i = 0; i < maxCount; i++)
        {
            if(i == maxCount / 2 - 1)
            {
                StartCoroutine(TextManipulator.TypeEffecting(levelLog, " -> " + str));
            }
            else if(i == maxCount / 2)
            {
                StartCoroutine(TextManipulator.EraseEffecting(levelLog, str.Length));
            }

            particles[indexs[i]].SetActive(true);
            particles[indexs[i]].transform.position = positionList[indexs[i]];

            yield return delay;
        }

        allowClose = true;

        prompt.gameObject.SetActive(true);

        StartCoroutine(UIElementUtility.BlinkText(prompt, minAlpha, maxAlpha, duration, false));
    }
}