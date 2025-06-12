using UnityEditor;
[CustomEditor(typeof(MonsterStat_SO))]
public class MonsterStatSO_Editor : Editor
{
    private SerializedProperty show;
    private SerializedProperty value;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("hasExtraObject");
        value = serializedObject.FindProperty("extraObject");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "extraObject");

        if(show.boolValue)
        {
            EditorGUILayout.PropertyField(value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}