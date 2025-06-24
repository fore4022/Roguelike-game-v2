using TMPro;
public class SkillPoints_UI : UserInterface
{
    private TextMeshProUGUI skillPoints;

    public override void SetUserInterface()
    {
        skillPoints = GetComponent<TextMeshProUGUI>();

        Managers.UI.Hide<SkillPoints_UI>();
    }
    protected override void Enable()
    {
        SkillPointsUpdate();
    }
    public void SkillPointsUpdate()
    {
        skillPoints.text = $"Skill Points : {Managers.Game.inGameData.player.LevelUpCount}";
    }
}