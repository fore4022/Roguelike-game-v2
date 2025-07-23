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