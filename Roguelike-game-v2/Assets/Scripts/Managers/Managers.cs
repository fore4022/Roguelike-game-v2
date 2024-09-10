using UnityEngine;
public class Managers : MonoBehaviour
{
    public static Managers managers;

    public GameManager game = new();
    public UserDataManager userData = new();
    public UIManager ui = new();

    public static Managers Instance
    {
        get 
        { 
            Init(); 

            return managers;
        }
    }
    public static GameManager Game
    {
        get
        {
            return Instance.game;
        }
    }
    public static UserDataManager UserData
    {
        get
        {
            return Instance.userData;
        }
    }
    public static UIManager UI
    {
        get
        {
            return Instance.ui;
        }
    }
    public static void Init()
    {
        if(managers == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null) 
            {
                go = new GameObject { name = "@Managers" }; 
            }

            managers = go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
        }
    }
}
