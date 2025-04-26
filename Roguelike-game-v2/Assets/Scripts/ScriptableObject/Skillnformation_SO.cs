using UnityEditor;
using UnityEngine;
[System.Serializable]
public class SkillInformation
{
    [HideInInspector]
    public Sprite icon;

    public string type;
    public string explanation;

    public SkillInformation(Skillnformation_SO info)
    {
        icon = Util.LoadingToPath<Sprite>(info.spritePath);
        type = info.attackInfo.type;
        explanation = info.attackInfo.explanation;
    }
}
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Create New SkillInformation_SO")]
public class Skillnformation_SO : ScriptableObject
{
    public SkillInformation attackInfo;

    [HideInInspector]
    public string spritePath;

#if UNITY_EDITOR
    public Sprite sprite;

    public void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(sprite == null)
            {
                ValidateUntilReady();
            }
            else
            {
                Validate();
            }
        };
    }
    public void Validate()
    {
        spritePath = $"Assets/Sprites/Icon/{sprite.name}.asset";
    }
#endif
}