using UnityEngine;
public class Game_Manager
{
    public PlayerDataManage playerData = null;
    public AttackDataManage attackData = null;
    public DifficultyScaler difficultyScaler = null;

    public StageInformation_SO stageInformation;

    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Set();

        this.stageInformation = stageInformation;

        InGameDataLoad.GetInGameData();
    }
    public void Set()
    {
        playerData = new();
        attackData = new();
        difficultyScaler = new();
    }
    public void GameStart()
    {
        Time.timeScale = 1;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();
    }
    public void GameEnd()
    {
        playerData = null;
        attackData = null;
        difficultyScaler = null;

        stageInformation = null;

        player = null;

        Time.timeScale = 0;
    }
}
//player.Set();