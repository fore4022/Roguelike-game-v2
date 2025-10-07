using System.Collections;
using TMPro;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    [Header("Step_1")]
    [SerializeField]
    private GameObject panel;
    private TextMeshProUGUI text;

    private float step_Index = 0;
            
    private void Awake()
    {
        // if user data == null

        StartCoroutine(Initializing());
    }
    public void PlayTutorial()
    {
        step_Index = 0;

        StartCoroutine(PlayingTutorial());
    }
    private void Step_1()
    {

    }
    private void Step_2()
    {

    }
    private void Step_3()
    {

    }
    private void Step_4()
    {

    }
    private void Step_5()
    {

    }
    private IEnumerator Initializing()
    {
        yield return new WaitUntil(() => Managers.Main.StageDatas != null);

        // == true

        PlayTutorial();
    }
    private IEnumerator PlayingTutorial()
    {
        Step_1();

        yield return null;

        Step_2();

        yield return null;

        Step_3();

        yield return null;

        Step_4();

        yield return null;

        Step_5();

        yield return null;
    }
}