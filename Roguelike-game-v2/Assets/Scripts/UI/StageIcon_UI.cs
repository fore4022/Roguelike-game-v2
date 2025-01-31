using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageIcon_UI : UserInterface
{
    [SerializeField]
    private TextMeshProUGUI sceneName;
    [SerializeField]
    private Image map_1;
    [SerializeField]
    private Image map_2;
    [SerializeField]
    private Image flag;
    [SerializeField]
    private Image enviroment;

    private Stage_SO stage;

    public override void SetUserInterface()
    {
        sceneName = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        Init();
    }
    private void Init()
    {
        stage = Managers.Main.gameData.GetStageSO(Managers.UserData.GetUserData.current_StageName);

        Set();
    }
    public void UpdateUI(string stageName)
    {
        stage = Managers.Main.gameData.GetStageSO(stageName);

        Set();
    }
    private void Set()
    {
        MapSprite_SO mapSprite = stage.mapSprite;

        sceneName.text = stage.stageName;
        map_1.sprite = mapSprite.map_1;
        map_2.sprite = mapSprite.map_2;
        flag.sprite = mapSprite.flag;
        enviroment.sprite = mapSprite.enviroment;
    }
}