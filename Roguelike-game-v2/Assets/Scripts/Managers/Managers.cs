using UnityEngine;
public class Managers : MonoBehaviour
{
    public static Managers managers;

    public new Audio_Manager audio = new();
    public Game_Manager game = new();
    public UI_Manager ui = new();
    public UserData_Manager userData = new();
    public Scene_Manager scene = new();
    public MainScene_Manager main = new();

    public static Managers Instance
    {
        get 
        { 
            Init();     

            return managers;
        }
    }
    public static Audio_Manager Audio { get { return Instance.audio; } }
    public static Game_Manager Game { get { return Instance.game; } }
    public static UI_Manager UI { get { return Instance.ui; } }
    public static UserData_Manager UserData { get { return Instance.userData; } }
    public static Scene_Manager Scene { get { return Instance.scene; } }
    public static MainScene_Manager Main { get { return Instance.main; } }
    public static void Init()
    {
        if(managers == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null) 
            {
                go = new GameObject { name = "@Managers" };
            }

            managers = go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
        }
    }
}