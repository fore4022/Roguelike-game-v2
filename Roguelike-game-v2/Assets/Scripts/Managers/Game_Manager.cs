using System;
using System.Collections;
using UnityEngine;
public class Game_Manager
{
    public AttackCasterManage attackCasterManage = null;
    public InGameDataManage inGameData = null;
    public DifficultyScaler difficultyScaler = null;
    public StageInformation_SO stageInformation;
    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;
    public Action onStageReset;

    private int userExp = 0;
    private bool gameOver = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
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
        inGameData.init.GetInGameData();
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        gameOver = false;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();
        inGameData.player.SetLevel();

        Managers.UI.ShowUI<LevelUp_UI>();
    }
    public void ReStart()
    {
        UserExp = 0;
        gameOver = false;
        
        attackCasterManage.StopAllCaster();
        inGameData.init.objectPool.ReSetting();

        attackCasterManage = new();
        difficultyScaler = new();
        inGameData.attack = new();

        Util.GetMonoBehaviour().StartCoroutine(ReSetting());
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;

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

        player.Reset();
        Managers.UI.HideUI<GameOver_UI>();
        InputActions.EnableInputAction<TouchControls>();

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() == null);

        Time.timeScale = 1;

        inGameTimer.ReStart();
        monsterSpawner.ReStart();
        //inGameData.player.SetLevel();
        onStageReset.Invoke();

        //Managers.UI.ShowUI<LevelUp_UI>();
    }
}