using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    [HideInInspector]
    public string infoPath;
    [HideInInspector]
    public string iconPath;
    [HideInInspector]
    public string stagePath;

    public StageInformation_SO information = null;
    public Icon_SO iconSprite = null;

#if UNITY_EDITOR
    public GameObject stage = null;

    public void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(stage == null || iconSprite == null || information == null)
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
        stagePath = stage.name;
    }
#endif
}