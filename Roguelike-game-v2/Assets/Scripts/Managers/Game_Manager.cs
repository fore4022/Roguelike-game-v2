using UnityEngine;
public class Game_Manager
{
    public InGameDataManage inGameData = null;
    public AttackCasterManage attackCasterManage = null;
    public DifficultyScaler difficultyScaler = null;
    public EnemyDetection enemyDetection = null;
    public Calculate calculate = null;
    public StageInformation_SO stageInformation;
    public InGameTimer inGameTimer;
    public MonsterSpawner monsterSpawner;
    public Player player;

    private void Set()
    {
        inGameData = new();
        attackCasterManage = new();
        difficultyScaler = new();
        enemyDetection = new();
        calculate = new();
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

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();
        inGameData.player.SetLevel();

        Managers.UI.ShowUI<LevelUp_UI>();
    }
    public void GameEnd()
    {
        inGameData = null;
        attackCasterManage = null;
        difficultyScaler = null;
        enemyDetection = null;
        calculate = null;
        stageInformation = null;
        player = null;
        Time.timeScale = 0;
    }
}