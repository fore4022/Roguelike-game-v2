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
        stage = Managers.Main.GetCurrentStage();

        Set();
    }
    public void UpdateUI(string stageName)
    {
        stage = Managers.Main.GameData.GetStageSO(stageName);

        Set();
    }
    private void Set()
    {
        IconSprite_SO iconSprite = stage.mapSprite;

        if(iconSprite.enviroment == null)
        {
            enviroment.gameObject.SetActive(false);
        }
        else
        {
            enviroment.gameObject.SetActive(true);

            enviroment.sprite = iconSprite.enviroment;
        }

        sceneName.text = stage.stageName;
        map_1.sprite = iconSprite.map_1;
        map_2.sprite = iconSprite.map_2;
    }
}