#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class UserLevel_EmbeddedData
{
    public static void AddEmbeddedData()
    {
        var asset = Selection.activeObject as UserLevel_SO;

        if(asset == null)
        {
            return;
        }

        var embeddedSO = ScriptableObject.CreateInstance<AttackInformation_SO>();

        AssetDatabase.AddObjectToAsset(embeddedSO, asset);
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
    }
}
#endif