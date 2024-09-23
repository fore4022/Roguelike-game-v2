public class DifficultyScaler
{
    private const int divideValue = 100;

    private float userLevelScale = 0;

    public float SpawnDelay { get { return Managers.Game.StageInformation.spawnDelay / GetDifficultyScale(); } }
    public float IncreaseStat { get { return Managers.Game.StageInformation.statScale * GetDifficultyScale(); } }
    private float GetDifficultyScale()
    {
        if(userLevelScale == 0)
        {
            userLevelScale = Managers.UserData.GetUserData.userLevel / divideValue;
        }

        float difficultyScale = Managers.Game.StageInformation.difficulty;

        difficultyScale += userLevelScale + GetTimeScale();

        return difficultyScale;
    }
    private float GetTimeScale()
    {
        float timeScale = Managers.Game.inGameTimer.GetMinutes / divideValue;

        return timeScale;
    }
}