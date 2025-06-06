using UnityEditor;
public class PlayerStatEditorWindow : EditorWindow
{
    private PlayerStat stat = new();

    [MenuItem("Tools/Player Stat Editor")]
    public static void Open()
    {
        GetWindow<PlayerStatEditorWindow>("Player Stat Editor");
    }
    private void OnGUI()
    {
        stat.increaseHealth = EditorGUILayout.FloatField("Increase Health", stat.increaseHealth);
        stat.increaseDamage = EditorGUILayout.FloatField("Increase Damage", stat.increaseDamage);
    }
}