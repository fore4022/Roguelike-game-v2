using System;
using System.Collections;
using UnityEngine;
public class Game_Manager
{
    public SkillCasterManage skillCasterManage;
    public InGameDataManage inGameData;
    public DifficultyScaler difficultyScaler;
    public DI_Container container;
    public GameEndEffect endEffect;
    public StageInformation_SO stageInformation;
    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public ObjectPool objectPool;
    public Player player;
    public AudioSource _bgm;
    public Action onStageReset;

    private int userExp = 0;
    private bool stageClear = false;
    private bool isPlaying = false;
    private bool gameOver = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
    public bool IsStageClear { get { return stageClear; } }
    public bool IsGameOver { get { return gameOver; } }
    private void Set()
    {
        skillCasterManage = new();
        inGameData = new();
        difficultyScaler = new();
        container = new();
    }
    public void DataLoad()
    {
        stageInformation = Managers.Main.GetCurrentStage().information;

        Set();
        Util.GetMonoBehaviour().StartCoroutine(inGameData.init.Initializing());
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        isPlaying = true;
        gameOver = false;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();
        inGameData.player.SetLevel();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
    }
    public void ReStart()
    {
        isPlaying = false;
        gameOver = false;
        
        skillCasterManage.StopAllCaster();
        inGameData.skill.Reset();

        skillCasterManage = new();
        difficultyScaler = new();

        Util.GetMonoBehaviour().StartCoroutine(ReSetting());
    }
    public void GameOver()
    {
        isPlaying = false;
        gameOver = true;

        Managers.UI.Hide<LevelUp_UI>();
        Managers.UI.Show<GameOver_UI>();
    }
    public void Clear()
    {
        skillCasterManage.StopAllCaster();

        skillCasterManage = null;
        inGameData = null;
        difficultyScaler = null;
        container = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        objectPool = null;
        userExp = 0;
    }
    public void IsStageCleared(float minutes)
    {
        if(minutes >= stageInformation.requiredTime)
        {
            stageClear = true;
            isPlaying = false;
            Managers.Game.inGameTimer.minuteUpdate -= IsStageCleared;

            GameOver();
        }
    }
    private IEnumerator ReSetting()
    {
        Managers.UI.Show<SceneLoading_UI>();

        yield return new WaitForSecondsRealtime(SceneLoading_UI.limitTime);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;
        Camera.main.orthographicSize = 6;
        Managers.Game.inGameData.player.Experience = 0;

        objectPool.ReSetting();
        player.Reset();
        inGameTimer.ReStart();
        Managers.UI.Hide<GameOver_UI>();
        InputActions.EnableInputAction<TouchControls>();

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Time.timeScale = 1;
        stageClear = false;
        isPlaying = true;

        monsterSpawner.ReStart();
        inGameData.player.SetLevel();
        onStageReset.Invoke();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
        Managers.UI.Show<HpSlider_UI>();
    }
}