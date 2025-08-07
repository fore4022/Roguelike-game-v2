using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageIcon_UI : UserInterface
{
    [SerializeField]
    private TextMeshProUGUI themaName;
    [SerializeField]
    private Image ground;
    [SerializeField]
    private Image cover;
    [SerializeField]
    private Image banner;
    [SerializeField]
    private Image monster;
    [SerializeField]
    private GameObject padlock;

    private Stage_SO so;

    private const string LockedText = "???";

    public override void SetUserInterface()
    {
        themaName = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

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

        ground.sprite = iconSprite.ground;
        monster.sprite = iconSprite.monster;

        if(state == StageState.Locked)
        {
            themaName.text = LockedText;
            cover.color = Color.black;
            ground.color = Color.black;
            monster.color = Color.black;

            banner.gameObject.SetActive(false);
            monster.gameObject.SetActive(true);
            padlock.SetActive(true);
        }
        else
        {
            themaName.text = so.name;
            cover.color = Color.white;
            ground.color = Color.white;
            monster.color = Color.white;

            if(state == StageState.Cleared)
            {
                banner.sprite = iconSprite.banner;
                banner.gameObject.SetActive(true);
                monster.gameObject.SetActive(false);
            }
            else
            {
                banner.gameObject.SetActive(false);
                monster.gameObject.SetActive(true);
            }

            padlock.SetActive(false);
        }

        if(iconSprite.cover == null)
        {
            cover.gameObject.SetActive(false);
        }
        else
        {
            cover.sprite = iconSprite.cover;

            cover.gameObject.SetActive(true);
        }
    }
}