using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Skill/Create New SkillInformation_SO")]
public class SkillInformation_SO : ScriptableObject
{
    [HideInInspector]
    public string spritePath;

    public SkillInformation info;

    public new string name;

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