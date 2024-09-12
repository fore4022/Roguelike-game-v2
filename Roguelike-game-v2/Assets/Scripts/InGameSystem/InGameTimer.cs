using System.Collections;
using UnityEngine;
public class InGameTimer : MonoBehaviour
{
    private Coroutine inGameTimer;

    private float elapsedTime;

    public float GetSeconds
    {
        get 
        {
            return elapsedTime % 60;
        }
    }
    public float GetMinutes 
    {
        get
        {
            return GetSeconds % 60;
        }
    }
    public float GetHours
    {
        get
        {
            return GetMinutes / 60;
        }
    }
    private void Start()
    {
        elapsedTime = 0;

        Managers.Game.inGameTimer = this;
    }
    public void Set()
    {
        StartTimer();
    }
    public void StartTimer()
    {
        inGameTimer = StartCoroutine(Timer());
    }
    public void StopTimer()
    {
        StopCoroutine(inGameTimer);
    }
    private IEnumerator Timer()
    {
        while(true)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}