using System;
using System.Collections;
using UnityEngine;
public class Game_Manager
{
    public SkillCaster_Manage skillCaster_Manage;
    public InGameData_Manage inGameData_Manage;
    public DamageLog_Manage damageLog_Manage;
    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;
    public GameOverEffect endEffect;
    public InGameTimer inGameTimer;
    public ObjectPool objectPool;
    public Player player;
    public StageInformation_SO stageInformation = null;

    public Action start;
    public Action restart;
    public Action over;

    private int userExp = 0;
    private bool isPlaying = false;
    private bool gameOver = false;
    private bool stageClear = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool Playing { get { return isPlaying; } set { isPlaying = value; } }
    public bool GameOver { get { return gameOver; } set { gameOver = value; } }
    public bool IsStageClear { get { return stageClear; } }
    private void Init()
    {
        skillCaster_Manage = new();
        inGameData_Manage = new();
        damageLog_Manage = new();
        difficultyScaler = new();
        monsterSpawner = new();
        endEffect = new();
        inGameTimer = new();
        objectPool = new();

        Set();
    }
    private void Set()
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
    public void DataLoad()
    {
        stageInformation = Managers.Main.GetCurrentStageSO().information;

        Init();
        Util.GetMonoBehaviour().StartCoroutine(inGameData_Manage.init.Initializing());
    }
    public void GameStart()
    {
        start.Invoke();

        isPlaying = true;
        gameOver = false;
        stageClear = false;

        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
    }
    public void ReStart()
    {
        isPlaying = false;
        gameOver = false;
        
        restart.Invoke();
        Util.GetMonoBehaviour().StartCoroutine(ReSetting());
    }
    public void Over()
    {
        Managers.Data.user.Exp += userExp;
        isPlaying = false;

        Managers.UI.Hide<LevelUp_UI>();
        Managers.UI.Show<GameOver_UI>();
    }
    public void GoMain()
    {
        skillCaster_Manage.StopAllCaster();
        inGameTimer.StopTimer();
        monsterSpawner.StopSpawn();
        objectPool.StopAllActions();

        skillCaster_Manage = null;
        inGameData_Manage = null;
        damageLog_Manage = null;
        difficultyScaler = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        objectPool = null;
        stageInformation = null;
        userExp = 0;

        Managers.Scene.LoadScene(SceneName.Main, false);
    }
    public void IsStageCleared(int minutes)
    {
        if(minutes >= stageInformation.requiredTime)
        {
            stageClear = true;
            isPlaying = false;
            Managers.Game.inGameTimer.minuteUpdate -= IsStageCleared;

            endEffect.StageClearEffect();
        }
    }
    private IEnumerator ReSetting()
    {
        Managers.UI.Show<SceneLoading_UI>();

        yield return new WaitForSecondsRealtime(SceneLoading_UI.limitTime);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;
        Camera.main.orthographicSize = CameraSizes.inGame * Camera_SizeScale.orthographicSizeScale;
        Managers.Game.inGameData_Manage.player.Experience = 0;
        isPlaying = true;

        objectPool.ReSetting();
        player.Reset();
        inGameTimer.ReStart();
        Managers.UI.Hide<GameOver_UI>();
        InputActions.EnableInputAction<TouchControls>();

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Time.timeScale = 1;
        userExp = 0;
        stageClear = false;

        monsterSpawner.ReStart();
        inGameData_Manage.player.SetLevel();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
        Managers.UI.Show<HpSlider_UI>();
    }
}