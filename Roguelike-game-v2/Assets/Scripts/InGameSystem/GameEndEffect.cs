using System.Collections;
using UnityEngine;
public class GameEndEffect : MonoBehaviour
{
    private const float effectDuration = 0.4f;

    public void GameOverEffect()
    {
        StartCoroutine(GameOverEffecting());
    }
    public void StageClearEffect()
    {
        StartCoroutine(StageClearEffecting());
    }
    private IEnumerator GameOverEffecting()
    {
        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        float totalTime = 0;

        while(totalTime > effectDuration)
        {
            yield return null;
        }
    }
    private IEnumerator StageClearEffecting()
    {
        float totalTime = 0;

        yield return null;
    }
}