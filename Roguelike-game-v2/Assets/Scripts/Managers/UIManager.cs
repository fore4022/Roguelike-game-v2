using System.Collections;
using UnityEngine;
public class UIManager
{
    private void Init()
    {
        GameObject go = GameObject.Find("UI");

        if(go == null)
        {
            go = new GameObject { name = "UI" };
        }
    }
    public static async void ShowUI()
    {
        Util.GetMonoBehaviour().StartCoroutine(CreateUI());
    }
    public static void CloseUI()
    {

    }
    public static async void LoadUI()
    {
        
    }
    public static async IEnumerator CreateUI()
    {


        yield return new WaitUntil(() => );
    }
}