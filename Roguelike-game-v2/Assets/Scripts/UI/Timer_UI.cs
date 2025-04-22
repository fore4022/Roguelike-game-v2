using System.Collections;
using TMPro;
using UnityEngine;
/// <summary>
/// <para>
/// �ΰ��� Ÿ�̸� ����
/// </para>
/// �÷��̾� ü���� 0 ���ϰ� �� ��� ������ �����Ѵ�.
/// �ð��� 0���� �ʱ�ȭ �ϱ� ���ؼ� �ʱ� ���� -1�� �����Ͽ���.
/// </summary>
public class Timer_UI : UserInterface
{
    private TextMeshProUGUI timer;

    private Coroutine timeUpdate;
    private int beforeSecond = -1;

    public override void SetUserInterface()
    {
        timer = GetComponent<TextMeshProUGUI>();
        timeUpdate = StartCoroutine(TimerUpdate());
    }
    protected override void Enable()
    {
        beforeSecond = -1;

        StopCoroutine(timeUpdate);

        timeUpdate = StartCoroutine(TimerUpdate());
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
                timer.text = $"{Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
            else
            {
                timer.text = $"{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
        }
    }
}