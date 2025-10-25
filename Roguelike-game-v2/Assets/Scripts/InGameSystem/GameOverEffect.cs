using System.Collections;
using UnityEngine;
/// <summary>
/// GameOver ø¨√‚
/// </summary>
public class GameOverEffect
{
    private readonly WaitForSeconds delay = new(duration);
    private const float duration = 0.4f;

    private float MaxOrthographicSize { get { return 6 * Camera_SizeScale.orthographicSizeScale; } }
    private float MinOrthographicSize { get { return 1.25f * Camera_SizeScale.orthographicSizeScale; } }
    public void EffectPlay()
    {
        Input_Manage.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        CoroutineHelper.StartCoroutine(GameOverEffecting());
    }
    public void StageClearEffect()
    {
        Input_Manage.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        CoroutineHelper.StartCoroutine(StageClearEffecting());
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

            Camera.main.orthographicSize = Mathf.Lerp(MaxOrthographicSize, MinOrthographicSize, totalTime / duration);

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

            Camera.main.orthographicSize = Mathf.Lerp(MinOrthographicSize, MaxOrthographicSize, totalTime / duration);

            yield return null;
        }

        Input_Manage.EnableInputAction<TouchControls>();
        Managers.UI.Show<HpSlider_UI>();
    }
}