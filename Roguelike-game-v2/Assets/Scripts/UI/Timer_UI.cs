using System.Collections;
using TMPro;
using UnityEngine;
public class Timer_UI : UserInterface
{
    private TextMeshProUGUI timer;
    private int beforeSecond = 0;

    public override void SetUserInterface()
    {
        timer = GetComponent<TextMeshProUGUI>();

        Managers.Game.onGameOver += Reset;
        StartCoroutine(TimerUpdate());
    }
    protected override void Enable()
    {
        StartCoroutine(TimerUpdate());
    }
    public void Reset()
    {
        beforeSecond = 0;
        StartCoroutine(TimerUpdate());
    }
    private IEnumerator TimerUpdate()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        yield return new WaitUntil(() => Managers.Game.player.Stat != null);

        while(Managers.Game.player.Stat.health > 0)
        {
            yield return new WaitUntil(() => beforeSecond != Managers.Game.inGameTimer.GetSeconds);

            beforeSecond = Managers.Game.inGameTimer.GetSeconds;

            if(Managers.Game.inGameTimer.GetHours == 0)
            {
                timer.text = $"{Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
            else
            {
                timer.text = $"{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
        }
    }
}