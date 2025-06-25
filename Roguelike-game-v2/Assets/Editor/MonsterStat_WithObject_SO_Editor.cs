using UnityEditor;
[CustomEditor(typeof(MonsterStat_WithObject_SO))]
public class MonsterStat_WithObject_SO_Editor : Editor
{
    private SerializedProperty show;
    private SerializedProperty value_1;
    private SerializedProperty value_2;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("hasExtraObject");
        value_1 = serializedObject.FindProperty("extraObject");
        value_2 = serializedObject.FindProperty("visualizer");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "extraObject", "visualizer");

        if(show.boolValue)
        {
            EditorGUILayout.PropertyField(value_1);
            EditorGUILayout.PropertyField(value_2);
        }

        serializedObject.ApplyModifiedProperties();
    }
}