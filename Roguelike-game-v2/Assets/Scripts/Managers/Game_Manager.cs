using System;
using System.Collections;
using UnityEngine;
public class Game_Manager
{
    public SkillCasterManage attackCasterManage = null;
    public InGameDataManage inGameData = null;
    public DifficultyScaler difficultyScaler = null;
    public StageInformation_SO stageInformation;
    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;
    public Action onStageReset;

    private int userExp = 0;
    private bool isPlaying = false;
    private bool gameOver = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
    public bool IsGameOver { get { return gameOver; } }
    private void Set()
    {
        attackCasterManage = new();
        inGameData = new();
        difficultyScaler = new();
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
        Managers.UI.ShowUI<LevelUp_UI>();
    }
    public void ReStart()
    {
        UserExp = 0;
        isPlaying = false;
        gameOver = false;
        
        attackCasterManage.StopAllCaster();

        attackCasterManage = new();
        difficultyScaler = new();

        Util.GetMonoBehaviour().StartCoroutine(ReSetting());
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        isPlaying = false;
        gameOver = true;

        Managers.UI.HideUI<LevelUp_UI>();
        Managers.UI.ShowUI<GameOver_UI>();
    }
    public void Clear()
    {
        attackCasterManage.StopAllCaster();

        attackCasterManage = null;
        inGameData = null;
        difficultyScaler = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        userExp = 0;
    }
    private IEnumerator ReSetting()
    {
        Managers.UI.ShowUI<SceneLoading_UI>();

        yield return new WaitForSecondsRealtime(SceneLoading_UI.limitTime);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;
        Camera.main.orthographicSize = 6;

        inGameData.init.objectPool.ReSetting();
        player.Reset();
        inGameTimer.ReStart();
        Managers.UI.HideUI<GameOver_UI>();
        InputActions.EnableInputAction<TouchControls>();

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() == null);

        Time.timeScale = 1;
        isPlaying = true;

        monsterSpawner.ReStart();
        inGameData.player.SetLevel();
        onStageReset.Invoke();
        Managers.UI.ShowUI<LevelUp_UI>();
    }
}