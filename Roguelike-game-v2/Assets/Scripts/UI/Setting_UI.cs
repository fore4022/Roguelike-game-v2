using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Setting_UI : UserInterface
{
    [SerializeField]
    private List<Sprite> _bgmSprite;
    [SerializeField]
    private List<Sprite> _fxSprite;
    [SerializeField]
    private Image _Bgm;
    [SerializeField]
    private Image _Fx;

    private const string _sceneName = "InGame";

    private bool _isInGame;

    public override void SetUserInterface()
    {
        _isInGame = Managers.Scene.CurrentSceneName == _sceneName ? true : false;

        Managers.UI.Hide<Setting_UI>();
    }
    protected override void Enable()
    {
        if(_isInGame)
        {
            Managers.UI.Get<PauseMenu_UI>().HideIcons();
        }

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
            _Bgm.sprite = _bgmSprite[0];
        }
        else
        {
            _Bgm.sprite = _bgmSprite[1];
        }
    }
    private void SfxUpdate()
    {
        if(Managers.UserData.data.FX)
        {
            _Fx.sprite = _fxSprite[0];
        }
        else
        {
            _Fx.sprite = _fxSprite[1];
        }
    }
    public void Confirm()
    {
        if(_isInGame)
        {
            Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        }

        Managers.UserData.Save();
        Managers.UI.Hide<Setting_UI>();
    }
}