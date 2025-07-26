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

        Debug.Log("a");

        yield return new WaitUntil(() => !Managers.UI.Get<GameOver_UI>().enabled);

        Debug.Log("b");

        cam.SetPosition(cam.position + new Vector3(0, 0.3f), duration);
    }
}