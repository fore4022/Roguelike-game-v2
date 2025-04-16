using System.Collections;
using UnityEngine;
public class InGameTimer : MonoBehaviour
{
    private Coroutine inGameTimer;
    private float elapsedTime;

    public int GetSeconds { get { return (int)elapsedTime % 60; } }
    public int GetMinutes { get { return (int)elapsedTime / 60; } }
    public int GetHours { get { return GetMinutes / 60; } }
    public int GetTotalMinutes { get { return GetMinutes + GetHours * 60; } }
    private void Awake()
    {
        Managers.Game.inGameTimer = this;
    }
    private void Start()
    {
        elapsedTime = 0;
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
        while(!Managers.Game.IsGameOver)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}