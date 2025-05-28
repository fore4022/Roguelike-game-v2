using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting_UI : UserInterface
{
    [SerializeField]
    private List<Sprite> _BgmSprite;
    [SerializeField]
    private List<Sprite> _FxSprite;
    [SerializeField]
    private Image _Bgm;
    [SerializeField]
    private Image _Fx;

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
        Managers.Audio.SetGroup(SoundTypes.BGM);
        BgmUpdate();
    }
    public void ToggleSFX()
    {
        Managers.Audio.SetGroup(SoundTypes.FX);
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
        if(Managers.UserData.data.FX)
        {
            _Fx.sprite = _FxSprite[0];
        }
        else
        {
            _Fx.sprite = _FxSprite[1];
        }
    }
    public void Confirm()
    {
        Managers.UserData.Save();
        Managers.UI.GetUI<PauseMenu_UI>().ShowIcons();
        Managers.UI.HideUI<Setting_UI>();
    }
}