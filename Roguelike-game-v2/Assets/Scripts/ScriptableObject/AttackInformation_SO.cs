using UnityEngine;
[System.Serializable]
public class AttackInformation
{
    [HideInInspector]
    public Sprite icon;

    public string type;
    public string explanation;

    public AttackInformation(AttackInformation_SO info)
    {
        icon = Util.LoadingToPath<Sprite>(info.spritePath);
        type = info.attackInfo.type;
        explanation = info.attackInfo.explanation;
    }
}
[CreateAssetMenu(fileName = "AttackInformation", menuName = "Create New SO/Create New AttackInformation_SO")]
public class AttackInformation_SO : ScriptableObject
{
    public AttackInformation attackInfo;

    [HideInInspector]
    public string spritePath;

#if UNITY_EDITOR
    public Sprite sprite;

    public void OnValidate()
    {
        spritePath = SetSpritePath();
    }
    public string SetSpritePath()
    {
        return $"Assets/Sprites/Icon/{sprite.name}.asset";
    }
#endif
}