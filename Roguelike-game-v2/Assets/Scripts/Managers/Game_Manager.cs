using UnityEngine;
public class Game_Manager
{
    public ObjectPool objectPool = null;
    public InGameDataManage inGameData = null;//
    public AttackCasterManage attackCasterManage = null;
    public DifficultyScaler difficultyScaler = null;
    public InGameDataInit inGameDataInit = null;

    public StageInformation_SO stageInformation;

    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    private void Set()
    {
        objectPool = new();
        inGameData = new();
        attackCasterManage = new();
        difficultyScaler = new();
        inGameDataInit = new();
    }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Set();

        this.stageInformation = stageInformation;

        inGameDataInit.GetInGameData();
    }
    public void GameStart()
    {
        Time.timeScale = 1;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();

        inGameData.playerData.SetLevel();
    }
    public void GameEnd()
    {
        objectPool = null;
        inGameData = null;
        attackCasterManage = null;
        difficultyScaler = null;
        inGameDataInit = null;

        stageInformation = null;

        player = null;

        Time.timeScale = 0;
    }
}