using UnityEngine;
public class Managers : MonoBehaviour
{
    public static Managers managers;

    public static Managers Instatnce
    {
        get 
        { 
            Init(); 

            return managers;
        }
    }
    public static void Init()
    {
        if(managers == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null) { go = new GameObject { name = "@Managers" }; }

            managers = go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
        }
    }
}
