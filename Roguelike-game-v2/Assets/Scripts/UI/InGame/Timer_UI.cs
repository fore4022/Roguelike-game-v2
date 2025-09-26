using TMPro;
/// <summary>
/// <para>
/// �ΰ��� Ÿ�̸� ����
/// </para>
/// InGameTimer�� timerUpdate�� ��ϵǾ��ִ�.
/// </summary>
public class Timer_UI : UserInterface
{
    private TextMeshProUGUI timer;

    public override void SetUserInterface()
    {
        timer = GetComponent<TextMeshProUGUI>();

        Managers.Game.inGameTimer.timerUpdate += TimerUpdate;
    }
    private void TimerUpdate()
    {
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