using UnityEngine;
public class Game_Manager
{
    public PlayerDataManage playerData;
    public AttackDataManage attackData;
    public DifficultyScaler difficultyScaler;

    public StageInformation_SO stageInformation;

    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        this.stageInformation = stageInformation;

        InGameDataLoad.GetInGameData();
    }
    public void Set()
    {
        playerData = new();
        attackData = new();
        difficultyScaler = new();

        inGameTimer.Set();
        monsterSpawner.Set();
        player.Set();
    }
    public void GameStart()
    {
        Time.timeScale = 1;
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