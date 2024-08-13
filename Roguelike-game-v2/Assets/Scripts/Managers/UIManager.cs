using UnityEngine;
public class UIManager
{
    private Transform root;

    public Transform Root 
    {
        get 
        {
            FindRoot();

            return root;
        }
    }
    private void FindRoot()
    {
        GameObject go = GameObject.Find("@UI");

        if(go == null)
        {
            go = new GameObject { name = "@UI" };

            root = go.transform;
        }
    }
}