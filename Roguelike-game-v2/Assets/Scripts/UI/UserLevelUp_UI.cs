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
    [SerializeField]
    private TextMeshProUGUI prompt;

    private const float delaySec = 0.8f;
    private const float duration = 1.5f;
    private const int maxCount = 6;

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
        StartCoroutine(LevelTextEffecting(levelUpCount));
        StartCoroutine(ParticleEffecting());
    }
    private IEnumerator LevelTextEffecting(int levelUpCount)
    {
        string str = $"Lv. {Managers.UserData.data.Level - levelUpCount}";
        int length = str.Length;

        levelLog.text = str;

        yield return delay;

        str = $"Lv. {Managers.UserData.data.Level}";

        StartCoroutine(Typing.TypeEffecting(levelLog, " -> " + str));

        yield return delay;

        prompt.gameObject.SetActive(true);
        StartCoroutine(Typing.EraseEffecting(levelLog, length));
        StartCoroutine(UIElementUtility.BlinkText(prompt, duration, false));
    }
    private IEnumerator ParticleEffecting()
    {
        int[] indexs = Calculate.GetRandomValues(maxCount);

        while(true)
        {
            for(int i = 0; i < maxCount; i++)
            {
                if(!particles[i].activeSelf)
                {
                    particles[i].SetActive(true);
                    particles[i].transform.position = Calculate.GetRandomVector();

                    yield return delay;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}