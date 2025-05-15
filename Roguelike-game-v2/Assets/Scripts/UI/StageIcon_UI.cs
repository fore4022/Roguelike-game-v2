using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageIcon_UI : UserInterface
{
    [SerializeField]
    private TextMeshProUGUI sceneName;
    [SerializeField]
    private Image ground_1;
    [SerializeField]
    private Image ground_2;
    [SerializeField]
    private Image flag;
    [SerializeField]
    private Image monster;
    [SerializeField]
    private GameObject padlock;

    private Stage_SO so;

    private const string LockedText = "???";

    public override void SetUserInterface()
    {
        sceneName = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        UpdateUI(0);
    }
    public void UpdateUI(int sign)
    {
        so = Managers.Main.GetCurrentStage(sign);

        Set();
    }
    private void Set()
    {
        StageState state = Managers.UserData.data.GetStageState();
        Icon_SO iconSprite = so.iconSprite;

        ground_1.sprite = iconSprite.ground_1;
        monster.sprite = iconSprite.monster;

        if(state == StageState.Locked)
        {
            sceneName.text = LockedText;
            ground_1.color = Color.black;
            ground_2.color = Color.black;
            monster.color = Color.black;

            flag.gameObject.SetActive(false);
            monster.gameObject.SetActive(true);
            padlock.SetActive(true);
        }
        else
        {
            sceneName.text = so.stagePath;
            ground_1.color = Color.white;
            ground_2.color = Color.white;
            monster.color = Color.white;

            if(state == StageState.Cleared)
            {
                flag.sprite = iconSprite.flag;
                flag.gameObject.SetActive(true);
            }
            else
            {
                flag.gameObject.SetActive(false);
            }

            monster.gameObject.SetActive(true);
            padlock.SetActive(false);
        }

        if(iconSprite.ground_2 == null)
        {
            ground_2.gameObject.SetActive(false);
        }
        else
        {
            ground_2.sprite = iconSprite.ground_2;

            ground_2.gameObject.SetActive(true);
        }
    }
}