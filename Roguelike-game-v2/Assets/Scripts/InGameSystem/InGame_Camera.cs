using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// InGame Camera 구현
/// </para>
/// 플레이어를 따라서 이동, GameOver 카메라 연출
/// </summary>
public class InGame_Camera : MonoBehaviour
{
    private GameObject player = null;

    private const float duration = 0.5f;
    private const float zpos = -10;

    private Coroutine cameraScale = null;

    private float targetCameraSize { get { return 1.25f * Camera_SizeScale.orthographicSizeScale; } }
    private void Start()
    {
        Managers.Game.onStageReset += Reset;
    }
    private void Update()
    {
        if(player == null)
        {
            if(Managers.Game.player != null)
            {
                player = Managers.Game.player.gameObject;
            }
            else
            {
                return;
            }
        }

        if(!Managers.Game.player.Death)
        {
            if(Managers.Game.Playing)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zpos);
            }
        }
        else if(cameraScale == null)
        {
            cameraScale = StartCoroutine(SetCameraScale());
        }
    }
    private void Reset()
    {
        cameraScale = null;
    }
    private IEnumerator SetCameraScale()
    {
        float totalTime = 0;
        float currentCameraSize = Camera.main.orthographicSize;

        while(totalTime != duration)
        {
            totalTime += Time.unscaledDeltaTime;

            if(totalTime > duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(currentCameraSize, targetCameraSize, totalTime / duration);

            yield return null;
        }
    }
}