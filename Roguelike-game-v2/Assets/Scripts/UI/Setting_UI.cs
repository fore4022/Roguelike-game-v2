using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting_UI : UserInterface
{
    [SerializeField]
    private List<Sprite> _BgmSprite;
    [SerializeField]
    private List<Sprite> _SfxSprite;
    [SerializeField]
    private Image _Bgm;
    [SerializeField]
    private Image _Sfx;

    public override void SetUserInterface()
    {
        Managers.UI.HideUI<Setting_UI>();
    }
    protected override void Enable()
    {
        BgmUpdate();
        SfxUpdate();
    }
    public void ToggleBGM()
    {
        Managers.UserData.data.SetBGM();
        BgmUpdate();
    }
    public void ToggleSFX()
    {
        Managers.UserData.data.SetSFX();
        SfxUpdate();
    }
    private void BgmUpdate()
    {
        if(Managers.UserData.data.BGM)
        {
            _Bgm.sprite = _BgmSprite[0];
        }
        else
        {
            _Bgm.sprite = _BgmSprite[1];
        }
    }
    private void SfxUpdate()
    {
        if(Managers.UserData.data.SFX)
        {
            _Sfx.sprite = _SfxSprite[0];
        }
        else
        {
            _Sfx.sprite = _SfxSprite[1];
        }
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.GetUI<PauseMenu_UI>().ShowIcons();
        Managers.UI.HideUI<Setting_UI>();
    }
}