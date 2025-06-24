using TMPro;
public class LevelText_InGame_UI : UserInterface
{
    private TextMeshProUGUI _levelText;

    public override void SetUserInterface()
    {
        _levelText = GetComponent<TextMeshProUGUI>();

        Managers.Game.inGameData.player.levelUpdate += LevelUpdate;
    }
    public void LevelUpdate()
    {
        _levelText.text = $"Lv. {Managers.Game.inGameData.player.Level}";
    }
}