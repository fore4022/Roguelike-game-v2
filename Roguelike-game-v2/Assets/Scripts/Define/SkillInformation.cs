using UnityEngine;
[System.Serializable]
public class SkillInformation
{
    [HideInInspector]
    public Sprite icon;

    public GameObject go;

    public string name;
    public string explanation;

    public SkillInformation(SkillInformation_SO so)
    {
        icon = Util.LoadingToPath<Sprite>(so.spritePath);
        go = so.info.go;
        name = so.info.name;
        explanation = so.info.explanation;
    }
    public string type { get { return go.name; } }
}