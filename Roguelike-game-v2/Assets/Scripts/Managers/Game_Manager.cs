using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// ���� �Ŵ����� ���� �ý��� ���� ���� �� ������ �帧�� ���¸� ����
/// </summary>
public class Game_Manager
{
    public SkillCaster_Manage skillCaster_Manage;
    public InGameData_Manage inGameData_Manage;
    public DamageLog_Manage damageLog_Manage;
    public ScriptableObject_Manage so_Manage;
    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;
    public GameEffect effect;
    public InGameTimer inGameTimer;
    public ObjectPool objectPool;
    public GameSetter gameSetter;
    public Player player;
    public StageInformation_SO stageInformation = null;

    public Action start;
    public Action restart;
    public Action over;

    private Coroutine reSetting = null;
    private int userExp = 0;
    private bool isPlaying = false;
    private bool gameOver = false;
    private bool stageClear = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool Playing { get { return isPlaying; } set { isPlaying = value; } }
    public bool GameOver { get { return gameOver; } }
    public bool IsStageClear { get { return stageClear; } }
    // ���� �Ŵ��� �ʱ�ȭ
    private void Init()
    {
        skillCaster_Manage = new();
        inGameData_Manage = new();
        damageLog_Manage = new();
        so_Manage = new();
        difficultyScaler = new();
        monsterSpawner = new();
        effect = new();
        inGameTimer = new();
        objectPool = new();
        gameSetter = new();

        SetEvent();
    }
    // �̺�Ʈ ���
    private void SetEvent()
    {
        // start
        start += inGameTimer.StartTimer;
        start += monsterSpawner.StartSpawn;
        start += inGameData_Manage.player.SetLevel;
        // restart
        restart += skillCaster_Manage.StopAllCaster;
        restart += inGameData_Manage.skill.Reset;
        restart += skillCaster_Manage.Reset;
    }
    // ���� ���� �ҷ����� �� �ʱ�ȭ
    public void InitGame()
    {
        stageInformation = Managers.Main.GetCurrentStageSO().information;
        isPlaying = false;

        Init();
        CoroutineHelper.Start(gameSetter.Initializing());
    }
    // �̺�Ʈ ȣ�� �� UI Ȱ��ȭ
    public void GameStart()
    {
        isPlaying = true;
        gameOver = false;
        stageClear = false;

        start.Invoke();

        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
    }
    // �̺�Ʈ ȣ��, ���� ���� �� ���� �缳��
    public void ReStart()
    {
        isPlaying = false;
        gameOver = false;
        
        restart.Invoke();
        CoroutineHelper.Start(ReStarting());
    }
    // �̺�Ʈ ȣ��, ���� ȿ�� ���, ��� ǥ��
    public void Over()
    {
        Managers.Data.user.Exp += userExp;
        isPlaying = false;

        if(!player.Death)
        {
            effect.StageClear();
        }
        else
        {
            gameOver = true;
            effect.StageFailed();
        }

        over?.Invoke();

        Input_Manage.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();
    }
    // ���� �Ŵ��� ���� �ߴ�, ���� �Ŵ��� ����
    public void Clear()
    {
        start = null;
        restart = null;
        over = null;

        skillCaster_Manage.StopAllCaster();
        inGameTimer.StopTimer();
        monsterSpawner.StopSpawn();
        objectPool.StopAllActions();

        skillCaster_Manage = null;
        inGameData_Manage = null;
        damageLog_Manage = null;
        so_Manage = null;
        difficultyScaler = null;
        inGameTimer = null;
        monsterSpawner = null;
        objectPool = null;
        gameSetter = null;
        stageInformation = null;
        userExp = 0;
    }
    // �������� Ŭ���� ���� Ȯ��
    public void IsStageCleared(int minutes)
    {
        if(minutes >= stageInformation.requiredTime)
        {
            stageClear = true;
            isPlaying = false;
            Managers.Game.inGameTimer.minuteUpdate -= IsStageCleared;

            Over();
        }
    }
    // ���� �缳�� ��� ���� �����
    private IEnumerator ReStarting()
    {
        reSetting = CoroutineHelper.Start(ReSetting());

        yield return null;

        yield return new WaitUntil(() => reSetting == null);

        Time.timeScale = 1;
        userExp = 0;
        stageClear = false;

        monsterSpawner.ReStart();
        inGameData_Manage.player.SetLevel();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
        Managers.UI.Show<HpSlider_UI>();
    }
    // ���� �Ŵ���, UI, ���� �ʱ�ȭ
    private IEnumerator ReSetting()
    {
        Managers.UI.Show<LoadingOverlay_UI>();

        yield return new WaitUntil(() => Managers.UI.Get<LoadingOverlay_UI>() != null);

        yield return new WaitUntil(() => !Managers.UI.Get<LoadingOverlay_UI>().IsFadeIn);

        objectPool.ReSetting();
        player.Reset();
        Managers.UI.Hide<GameOver_UI>();
        Managers.UI.Get<LoadingOverlay_UI>().FadeOut();

        Camera.main.orthographicSize = CameraSizes.inGame * Camera_SizeScale.orthographicSizeScale;
        Managers.Game.inGameData_Manage.player.Experience = 0;
        isPlaying = true;

        inGameTimer.ReStart();
        Input_Manage.EnableInputAction<TouchControls>();

        reSetting = null;
    }
}