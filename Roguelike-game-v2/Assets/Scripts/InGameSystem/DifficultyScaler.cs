public class DifficultyScaler
{
    public float SpawnDelay { get { return Managers.Game.stageInformation.spawnDelay / GetDifficultyScale(); } }
    public float IncreaseStat { get { return Managers.Game.stageInformation.statScale * GetDifficultyScale(); } }
    private float GetDifficultyScale()
    {
        return Managers.Game.stageInformation.difficulty + Managers.Game.inGameTimer.GetMinutes / 10;
    }
}