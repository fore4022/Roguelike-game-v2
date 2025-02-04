using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    public StageInformation_SO stageInformation;
    public IconSprite_SO mapSprite;

    public string stageName;
}