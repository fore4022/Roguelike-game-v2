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

        UpdateUI(0);
    }
    public void UpdateUI(int sign)
    {
        stage = Managers.Main.GetCurrentStage(sign);

        Set();
    }
    private void Set()
    {
        IconSprite_SO iconSprite = stage.mapSprite;

        sceneName.text = stage.stageName;
        map_1.sprite = iconSprite.map_1;
        enviroment.sprite = iconSprite.enviroment;

        if (!Managers.UserData.data.isClear())
        {
            enviroment.gameObject.SetActive(true);
            flag.gameObject.SetActive(false);
        }
        else
        {
            enviroment.gameObject.SetActive(false);
            flag.gameObject.SetActive(true);

            flag.sprite = iconSprite.flag;
        }

        if (iconSprite.map_2 == null)
        {
            map_2.gameObject.SetActive(false);
        }
        else
        {
            map_2.gameObject.SetActive(true);

            map_2.sprite = iconSprite.map_2;
        }
    }
}