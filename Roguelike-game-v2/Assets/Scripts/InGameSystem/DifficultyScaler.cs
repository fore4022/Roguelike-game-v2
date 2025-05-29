using UnityEngine;
public class DifficultyScaler
{
    private const float increaseRate = 0.05f;
    private const int criticalMinute = 10;

    public float SpawnDelay { get { return Managers.Game.stageInformation.spawnDelay / GetDifficultyScale(); } }
    public float IncreaseStat { get { return Managers.Game.stageInformation.statScale * GetDifficultyScale(); } }
    private float GetDifficultyScale()
    {
        float minute = Managers.Game.inGameTimer.GetMinutes;

        Debug.Log((1 + increaseRate * Managers.Game.stageInformation.difficulty) * (1 + increaseRate * minute + (minute > criticalMinute ? 0.001f * Mathf.Pow(minute - criticalMinute, 3) : 0)));

        return (1 + increaseRate * Managers.Game.stageInformation.difficulty) * (1 + increaseRate * minute + (minute > criticalMinute ? 0.001f * Mathf.Pow(minute - criticalMinute, 3) : 0));
    }
}