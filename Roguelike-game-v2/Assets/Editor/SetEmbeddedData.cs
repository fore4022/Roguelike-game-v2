#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class SetEmbeddedData
{
    [MenuItem("Assets/Embedded Data/Set/UserLevel_SO")]
    public static void SetUserLevel()
    {
        var mainAsset = Selection.activeObject as UserLevel_SO;

        if(mainAsset == null)
        {
            return;
        }

        if(mainAsset.attackInformationList == null)
        {
            mainAsset.attackInformationList = new();
        }

        AttackInformation_SO embeddedSO;

        for(int i = mainAsset.attackInformationList.Count; i < mainAsset.count; i++)
        {
            embeddedSO = ScriptableObject.CreateInstance<AttackInformation_SO>();
            embeddedSO.name = $"AttackInformation_{i}";

            AssetDatabase.AddObjectToAsset(embeddedSO, mainAsset);
            mainAsset.attackInformationList.Add(embeddedSO);
        }

        EditorUtility.SetDirty(mainAsset);
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Embedded Data/Remove/UserLevel_SO")]
    public static void RemoveEmbeddedData_UserLevel()
    {
        UserLevel_SO mainAsset = Selection.activeObject as UserLevel_SO;

        string assetPath = AssetDatabase.GetAssetPath(mainAsset);

        var subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);

        foreach(var selectedObject in subAssets)
        {
            var embeddedSO = selectedObject as AttackInformation_SO;

            if(embeddedSO != null)
            {
                if(mainAsset.attackInformationList.Contains(embeddedSO))
                {
                    mainAsset.attackInformationList.Remove(embeddedSO);
                    AssetDatabase.RemoveObjectFromAsset(embeddedSO);
                    Object.DestroyImmediate(embeddedSO, true);
                }
            }
        }

        EditorUtility.SetDirty(mainAsset);
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Embedded Data/Set/UserLevels_SO")]
    public static void SetUserLevels()
    {
        var mainAsset = Selection.activeObject as UserLevels_SO;

        if (mainAsset == null)
        {
            return;
        }

        if (mainAsset.levelInfo == null)
        {
            mainAsset.levelInfo = new();
        }

        UserLevel_SO embeddedSO;

        for (int i = mainAsset.levelInfo.Count; i < mainAsset.count; i++)
        {
            embeddedSO = ScriptableObject.CreateInstance<UserLevel_SO>();
            embeddedSO.name = $"UserLevel_{i}";

            AssetDatabase.AddObjectToAsset(embeddedSO, mainAsset);
            mainAsset.levelInfo.Add(embeddedSO);
        }

        EditorUtility.SetDirty(mainAsset);
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Embedded Data/Remove/UserLevels_SO")]
    public static void RemoveEmbeddedData_UserLevels()
    {
        var mainAsset = Selection.activeObject as UserLevels_SO;

        if (mainAsset == null)
        {
            return;
        }

        foreach (var selectedObject in Selection.objects)
        {
            var embeddedSO = selectedObject as UserLevel_SO;

            if (embeddedSO != null)
            {
                if (mainAsset.levelInfo.Contains(embeddedSO))
                {
                    mainAsset.levelInfo.Remove(embeddedSO);
                }

                AssetDatabase.RemoveObjectFromAsset(embeddedSO);
                Object.DestroyImmediate(embeddedSO, true);
            }
        }

        EditorUtility.SetDirty(mainAsset);
        AssetDatabase.SaveAssets();
    }
}
#endif