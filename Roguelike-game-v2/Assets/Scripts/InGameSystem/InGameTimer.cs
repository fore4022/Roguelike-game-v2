using System;
using System.Collections;
using UnityEngine;
public class InGameTimer : MonoBehaviour
{
    public Action<float> minuteUpdate = null;

    private Coroutine inGameTimer;
    private float seconds = 0;
    private int minutes = 0;
    private int hours = 0;

    public int GetSeconds { get { return (int)seconds; } }
    public int GetMinutes { get { return minutes; } }
    public int GetHours { get { return hours; } }
    public int GetTotalMinutes { get { return GetMinutes + GetHours * 60; } }
    private void Awake()
    {
        Managers.Game.inGameTimer = this;
        minuteUpdate += Managers.Game.IsStageCleared;
    }
    public void StartTimer()
    {
        inGameTimer = StartCoroutine(Timer());
    }
    public void ReStart()
    {
        seconds = minutes = hours = 0;

        StopCoroutine(inGameTimer);

        inGameTimer = StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        while(!Managers.Game.GameOver)
        {
            seconds += Time.deltaTime;

            if(seconds >= 60)
            {
                seconds -= 60;
                minutes++;

                minuteUpdate?.Invoke(minutes);

                if(minutes >= 60)
                {
                    minutes -= 60;
                    hours++;
                }
            }

            yield return null;
        }
    }
}