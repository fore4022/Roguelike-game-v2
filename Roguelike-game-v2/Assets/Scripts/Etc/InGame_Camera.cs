using System.Collections;
using UnityEngine;
public class InGame_Camera : MonoBehaviour
{
    private GameObject player = null;

    private const float duration = 0.5f;
    private const float targetCameraSize = 2f;
    private const float zpos = -10;

    private Coroutine cameraScale = null;

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
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zpos);
        }
        else if(cameraScale == null)
        {
            cameraScale = StartCoroutine(SetCameraScale());
        }
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