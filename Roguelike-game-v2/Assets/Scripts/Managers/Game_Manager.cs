using System;
using System.Collections;
using UnityEngine;
public class Game_Manager
{
    public SkillCaster_Manage skillCaster_Manage; // 1
    public InGameData_Manage inGameData_Manage; // 1
    public DamageLog_Manage damageLog_Manage; // 1
    public DifficultyScaler difficultyScaler; // 1
    public GameOverEffect endEffect; // -> GameOverUI
    public StageInformation_SO stageInformation; //OK
    public InGameTimer inGameTimer; // 0
    public MonsterSpawner monsterSpawner; // 0
    public ObjectPool objectPool; // 1
    public Player player; // 1
    //public AudioSource _bgm;

    public Action onStageReset;
    public Action start;
    public Action restart;
    public Action over;

    private int userExp = 0;
    private bool isPlaying = false;
    private bool gameOver = false;
    private bool stageClear = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool Playing { get { return isPlaying; } set { isPlaying = value; } }
    public bool GameOver { get { return gameOver; } set { gameOver = value; } }
    public bool IsStageClear { get { return stageClear; } }
    private void Set()
    {
        skillCaster_Manage = new();
        inGameData_Manage = new();
        damageLog_Manage = new();
        difficultyScaler = new();
    }
    public void DataLoad()
    {
        stageInformation = Managers.Main.GetCurrentStageSO().information;

        Set();
        Util.GetMonoBehaviour().StartCoroutine(inGameData_Manage.init.Initializing());
    }
    public void GameStart()
    {
        onStageReset.Invoke();

        Time.timeScale = 1;
        isPlaying = true;
        gameOver = false;
        stageClear = false;

        inGameTimer.StartTimer();
        monsterSpawner.StartSpawn();
        inGameData_Manage.player.SetLevel();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
    }
    public void ReStart()
    {
        isPlaying = false;
        gameOver = false;
        
        onStageReset.Invoke();
        skillCaster_Manage.StopAllCaster();
        inGameData_Manage.skill.Reset();

        skillCaster_Manage = new();
        difficultyScaler = new();

        Util.GetMonoBehaviour().StartCoroutine(ReSetting());
    }
    public void Over()
    {
        Managers.Data.user.Exp += userExp;
        isPlaying = false;

        Managers.UI.Hide<LevelUp_UI>();
        Managers.UI.Show<GameOver_UI>();
    }
    public void GoMain()
    {
        skillCaster_Manage.StopAllCaster();
        objectPool.StopAllActions();

        skillCaster_Manage = null;
        inGameData_Manage = null;
        damageLog_Manage = null;
        difficultyScaler = null;
        stageInformation = null;
        inGameTimer = null;
        monsterSpawner = null;
        objectPool = null;
        userExp = 0;

        Managers.Scene.LoadScene(SceneName.Main, false);
    }
    public void IsStageCleared(int minutes)
    {
        if(minutes >= stageInformation.requiredTime)
        {
            stageClear = true;
            isPlaying = false;
            Managers.Game.inGameTimer.minuteUpdate -= IsStageCleared;

            endEffect.StageClearEffect();
        }
    }
    private IEnumerator ReSetting()
    {
        Managers.UI.Show<SceneLoading_UI>();

        yield return new WaitForSecondsRealtime(SceneLoading_UI.limitTime);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;
        Camera.main.orthographicSize = CameraSizes.inGame * Camera_SizeScale.orthographicSizeScale;
        Managers.Game.inGameData_Manage.player.Experience = 0;
        isPlaying = true;

        objectPool.ReSetting();
        player.Reset();
        inGameTimer.ReStart();
        Managers.UI.Hide<GameOver_UI>();
        InputActions.EnableInputAction<TouchControls>();

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Time.timeScale = 1;
        userExp = 0;
        stageClear = false;

        monsterSpawner.ReStart();
        inGameData_Manage.player.SetLevel();
        onStageReset.Invoke();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
        Managers.UI.Show<HpSlider_UI>();
    }
}