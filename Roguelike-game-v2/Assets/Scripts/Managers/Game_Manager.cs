using UnityEngine;
public class Game_Manager
{
    public PlayerInformationManage playerInfo;
    public StageInformation_SO stageInformation;

    public DifficultyScaler difficultyScaler;
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
        Time.timeScale = 0;
    }
}