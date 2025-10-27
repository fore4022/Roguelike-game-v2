using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Game Stage/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    public SpawnPatternList_SO spawnPatternList;
    public SpawnMonsterList_SO spawnMonsterList;
    public AudioClip bgm;

    public Color skillRangeVisualizerColor = new(255, 50, 50);
    public float difficulty = 1;
    public float statScale = 1;
    public float spawnDelay;
    [Tooltip("Minute")]
    public int requiredTime;
    [Tooltip("Damage Text")]
    public Color damageTextColor = new(255, 255, 255, 255);
    [Tooltip("Skill Range VisualizerColor")]
    public bool isDefaultColor = true;

#if UNITY_EDITOR
    private int defaultAlpha = 200;

    private void OnValidate()
    {
        if(isDefaultColor)
        {
            skillRangeVisualizerColor = new Color32(255, 50, 50, (byte)defaultAlpha);
        }
        else
        {
            skillRangeVisualizerColor = new(skillRangeVisualizerColor.r, skillRangeVisualizerColor.g, skillRangeVisualizerColor.b, defaultAlpha / 255f);
        }
    }
#endif
}