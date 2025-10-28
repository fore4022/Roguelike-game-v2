using UnityEngine;
/// <summary>
/// <para>
/// 주요 매니저를 담는 스크립트
/// </para>
/// 싱글톤으로 구현
/// </summary>
public class Managers : MonoBehaviour
{
    public static Managers managers;

    public new Audio_Manager audio = new();
    public Game_Manager game = new();
    public UI_Manager ui = new();
    public Data_Manager data = new();
    public Scene_Manager scene = new();
    public Main_Manager main = new();

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
    public static Data_Manager Data { get { return Instance.data; } }
    public static Scene_Manager Scene { get { return Instance.scene; } }
    public static Main_Manager Main { get { return Instance.main; } }
    public static void Init()
    {
        if(managers == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null) 
            {
                go = new GameObject { name = "@Managers" };
            }

            if(!go.TryGetComponent(out managers))
            {
                managers = go.AddComponent<Managers>();
            }

            if(!go.GetComponent<Manager_Initializer>())
            {
                go.AddComponent<Manager_Initializer>();
            }

            DontDestroyOnLoad(go);
        }
    }
}