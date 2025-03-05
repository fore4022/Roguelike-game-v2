using UnityEngine;
public class Game_Manager
{
    public InGameDataManage inGameData = null;
    public AttackCasterManage attackCasterManage = null;
    public DifficultyScaler difficultyScaler = null;
    public StageInformation_SO stageInformation;
    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    private int userExp = 0;
    private bool gameOver = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool GameOver { get { return gameOver; } }
    private void Set()
    {
        inGameData = new();
        attackCasterManage = new();
        difficultyScaler = new();
    }
    public void DataLoad()
    {
        stageInformation = Managers.Main.GetCurrentStage().stageInformation;

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
    public void GameEnd()
    {
        Time.timeScale = 0;
        gameOver = true;

        Managers.UI.ShowUI<GameOver_UI>();
    }
    public void Clear()
    {
        inGameData = null;
        attackCasterManage = null;
        difficultyScaler = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        player = null;
        userExp = 0;
    }
}