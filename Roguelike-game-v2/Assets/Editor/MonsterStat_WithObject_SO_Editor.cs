using UnityEditor;
[CustomEditor(typeof(MonsterStat_WithObject_SO))]
public class MonsterStat_WithObject_SO_Editor : Editor
{
    private SerializedProperty show;
    private SerializedProperty value;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("hasExtraObject");
        value = serializedObject.FindProperty("extraObjects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "extraObjects");

        if(show.boolValue)
        {
            EditorGUILayout.PropertyField(value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}