using TMPro;
using UnityEngine;
public class StageIcon_UI : UserInterface
{
    [SerializeField]
    private TextMeshProUGUI sceneName;

    private Stage_SO stage;

    public Stage_SO SetStage_SO { set { stage = value; } }
    public override void SetUserInterface()
    {
        sceneName = Util.GetComponentInChildren<TextMeshProUGUI>(transform);
    }
    public void UpdateUI()
    {
        sceneName.text = stage.stageName;
        
        Instantiate(stage.mapIcon);
    }
}