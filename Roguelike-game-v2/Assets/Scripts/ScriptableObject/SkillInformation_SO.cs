using UnityEditor;
using UnityEngine;
[System.Serializable]
public class SkillInformation
{
    [HideInInspector]
    public Sprite icon;

    public string type;
    public string explanation;

    public SkillInformation(SkillInformation_SO info)
    {
        icon = Util.LoadingToPath<Sprite>(info.spritePath);
        type = info.skillInfo.type;
        explanation = info.skillInfo.explanation;
    }
}
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Create New SkillInformation_SO")]
public class SkillInformation_SO : ScriptableObject
{
    public SkillInformation skillInfo;

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