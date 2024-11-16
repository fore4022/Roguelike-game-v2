using System.Collections;
using TMPro;
using UnityEngine;
public class Timer_UI : UserInterface
{
    private TextMeshProUGUI timer;
    private Coroutine timerCoroutine;

    private int beforeSecond = 0;

    public override void SetUI()
    {
        timer = GetComponent<TextMeshProUGUI>();

        timerCoroutine = StartCoroutine(TimerUpdate());
    }
    private IEnumerator TimerUpdate()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        yield return new WaitUntil(() => Managers.Game.player.Stat != null);

        while(Managers.Game.player.Stat.health > 0)
        {
            yield return new WaitUntil(() => beforeSecond != Managers.Game.inGameTimer.GetSeconds);

            beforeSecond = Managers.Game.inGameTimer.GetSeconds;

            if (Managers.Game.inGameTimer.GetHours == 0)
            {
                timer.text = $"{Managers.Game.inGameTimer.GetMinutes} : {Managers.Game.inGameTimer.GetSeconds}";
            }
            else
            {
                timer.text = $"{Managers.Game.inGameTimer.GetHours} : {Managers.Game.inGameTimer.GetMinutes} : {Managers.Game.inGameTimer.GetSeconds}";
            }
        }
    }
}