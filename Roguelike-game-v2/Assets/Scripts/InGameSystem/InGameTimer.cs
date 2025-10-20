using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// InGame Timer으로 시간 변화에 따른 이벤트
/// </summary>
public class InGameTimer
{
    public Action<int> minuteUpdate = null;
    public Action timerUpdate = null;

    private Coroutine inGameTimer;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;

    public int GetSeconds { get { return seconds; } }
    public int GetMinutes { get { return minutes; } }
    public int GetHours { get { return hours; } }
    public int GetTotalMinutes { get { return GetMinutes + GetHours * 60; } }
    public InGameTimer()
    {
        Managers.Game.inGameTimer = this;
        minuteUpdate += Managers.Game.IsStageCleared;
    }
    public void StartTimer()
    {
        inGameTimer = Coroutine_Helper.StartCoroutine(Timer());
    }
    public void StopTimer()
    {
        Coroutine_Helper.StopCoroutine(inGameTimer);
    }
    public void ReStart()
    {
        seconds = minutes = hours = 0;

        Coroutine_Helper.StopCoroutine(inGameTimer);

        inGameTimer = Coroutine_Helper.StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        while(!Managers.Game.GameOver)
        {
            seconds++;

            if(Managers.Game.Playing)
            {
                timerUpdate?.Invoke();
            }

            if(seconds == 60)
            {
                seconds = 0;
                minutes++;

                minuteUpdate?.Invoke(minutes);

                if(minutes == 60)
                {
                    minutes = 0;
                    hours++;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
}