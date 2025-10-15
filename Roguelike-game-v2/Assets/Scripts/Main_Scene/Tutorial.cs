using System.Collections;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        yield return new WaitUntil(() => Managers.Data.data != null);

        if(!Managers.Data.data.Tutorial)
        {
            StartCoroutine(PlayingTutorial());
        }
    }
    private IEnumerator PlayingTutorial()
    {
        yield return new WaitUntil(() => true);

        //Managers.Data.data.Tutorial = true;

        Managers.UI.Hide<Tutorial_MaskImage_UI>();
    }
}