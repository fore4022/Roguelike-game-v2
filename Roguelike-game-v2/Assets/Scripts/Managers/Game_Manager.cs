using UnityEngine;
public class Game_Manager
{
    public StageInformation_SO stageInformation;
    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;
    public InGameTimer inGameTimer;
    public Player player;

    private string inGameScene = "InGame";

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        this.stageInformation = stageInformation;

        InGameDataLoad.GetInGameData(inGameScene);
    }
    public void Set()
    {
        difficultyScaler = new();

        monsterSpawner.Set();
        inGameTimer.Set();
        player.Set();
    }
    public void GameStart()
    {
        Time.timeScale = 1;
    }
    public void GameEnd()
    {
        Time.timeScale = 0;

        //
    }
}