using UnityEngine;
public class Game_Manager
{
    public ObjectPool objectPool = null;
    public PlayerDataManage playerData = null;
    public InGameDataManage inGameData = null;
    public AttackCasterManage attackCasterManage = null;
    public DifficultyScaler difficultyScaler = null;
    public AttackDataManage attackData = null;
    public InGameDataLoad inGameDataLoad = null;

    public StageInformation_SO stageInformation;

    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    private void Set()
    {
        objectPool = new();
        playerData = new();
        inGameData = new();
        attackCasterManage = new();
        difficultyScaler = new();
        attackData = new();
        inGameDataLoad = new();
    }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Set();

        this.stageInformation = stageInformation;

        inGameDataLoad.GetInGameData();
    }
    public void GameStart()
    {
        Time.timeScale = 1;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();

        playerData.SetLevel();
    }
    public void GameEnd()
    {
        objectPool = null;
        playerData = null;
        inGameData = null;
        attackCasterManage = null;
        difficultyScaler = null;
        attackData = null;
        inGameDataLoad = null;

        stageInformation = null;

        player = null;

        Time.timeScale = 0;
    }
}