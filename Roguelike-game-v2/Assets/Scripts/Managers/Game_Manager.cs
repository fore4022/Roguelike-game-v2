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

    private int userExp = 0;
    private bool gameOver = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool GameOver { get { return gameOver; } }
    private void Set()
    {
        attackCasterManage = new();
        inGameData = new();
        difficultyScaler = new();
    }
    public void DataLoad(bool isReStart = false)
    {
        stageInformation = Managers.Main.GetCurrentStage().stageInformation;

        Set();
        inGameData.init.GetInGameData(isReStart);
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
        attackCasterManage.StopAllCaster();

        attackCasterManage = null;
        inGameData = null;
        difficultyScaler = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        userExp = 0;
    }
}