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
        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        StartCoroutine(GameOverEffecting());
    }
    public void StageClearEffect()
    {
        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        StartCoroutine(StageClearEffecting());
    }
    private IEnumerator GameOverEffecting()
    {
        yield return delay;

        Managers.Game.GameOver = true;

        Managers.Game.Over();
    }
    private IEnumerator StageClearEffecting()
    {
        Transform cam = Camera.main.transform;

        float totalTime = 0;

        cam.SetPosition(cam.position + new Vector3(0, -0.3f), duration);

        while(totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime >= duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(6, 1.25f, totalTime / duration);

            yield return null;
        }

        yield return delay;

        Managers.Game.Over();

        yield return new WaitUntil(() => !Managers.UI.Get<GameOver_UI>().gameObject.activeSelf);

        totalTime = 0;

        cam.SetPosition(cam.position + new Vector3(0, 0.3f), duration);

        while(totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime >= duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(1.25f, 6, totalTime / duration);

            yield return null;
        }

        InputActions.EnableInputAction<TouchControls>();
        Managers.UI.Show<HpSlider_UI>();
    }
}