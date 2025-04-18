using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    public StageInformation_SO stageInformation;
    public IconSprite_SO mapSprite;

    [HideInInspector]
    public string stageName;

#if UNITY_EDITOR
    public GameObject stage;

    public void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(stage == null)
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
        stageName = stage.name;
    }
#endif
}