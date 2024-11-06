using UnityEngine;
public class Game_Manager
{
    public InGameDataManage inGameData = null;
    public ObjectPool objectPool = null;
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
        objectPool = new();
        attackCasterManage = new();
        difficultyScaler = new();
        enemyDetection = new();
        calculate = new();
    }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Set();

        this.stageInformation = stageInformation;

        inGameData.dataInit.GetInGameData();
    }
    public void GameStart()
    {
        Time.timeScale = 1;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();

        inGameData.playerData.SetLevel();

        Managers.UI.ShowUI<LevelSlider_UI>();
        Managers.UI.ShowUI<Timer_UI>();
    }
    public void GameEnd()
    {
        inGameData = null;
        objectPool = null;
        attackCasterManage = null;
        difficultyScaler = null;
        enemyDetection = null;
        calculate = null;

        stageInformation = null;

        player = null;

        Time.timeScale = 0;
    }
}