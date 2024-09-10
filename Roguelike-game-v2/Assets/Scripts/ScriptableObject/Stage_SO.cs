using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    public StageInformation_SO stageInformation_SO;

    public Sprite background;

    public string stageName;
    public float difficulty;
}