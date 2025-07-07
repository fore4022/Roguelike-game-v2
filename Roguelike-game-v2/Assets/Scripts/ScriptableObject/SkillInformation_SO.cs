using UnityEditor;
using UnityEngine;
[System.Serializable]
public class SkillInformation
{
    [HideInInspector]
    public Sprite icon;

    public string type;
    public string explanation;

    public SkillInformation(SkillInformation_SO so)
    {
        icon = Util.LoadingToPath<Sprite>(so.spritePath);
        type = so.info.type;
        explanation = so.info.explanation;
    }
}
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Create New SkillInformation_SO")]
public class SkillInformation_SO : ScriptableObject
{
    [HideInInspector]
    public string spritePath;

    public SkillInformation info;

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
        spritePath = $"Assets/Sprites/Icon/Skills/{sprite.name}.png";
    }
#endif
}