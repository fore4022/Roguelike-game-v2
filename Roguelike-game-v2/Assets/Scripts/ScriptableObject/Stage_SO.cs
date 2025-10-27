using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    [HideInInspector]
    public string infoPath;
    [HideInInspector]
    public string iconPath;

    public StageInformation_SO information = null;
    public Icon_SO iconSprite = null;

    public string stagePath;
    public new string name;

#if UNITY_EDITOR
    public void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(iconSprite == null || information == null)
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
        infoPath = information.name;
        iconPath = iconSprite.name;
    }
#endif
}