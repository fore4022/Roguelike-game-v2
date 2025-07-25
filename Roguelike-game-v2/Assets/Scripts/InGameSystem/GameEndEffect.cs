using System.Collections;
using UnityEngine;
public class GameEndEffect : MonoBehaviour
{
    private const float duration = 0.4f;

    private WaitForSeconds delay;

    private void Awake()
    {
        Managers.Game.endEffect = this;
    }
    private void Start()
    {
        delay = new(duration);
    }
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

        yield return delay;

        Managers.Game.GameOver();
    }
    private IEnumerator StageClearEffecting()
    {
        yield return null;
    }
}