using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Manager
{
    private Dictionary<string, GameObject> uiDictionary = new();

    private Transform Transform
    {
        get 
        {
            GameObject go = GameObject.Find("UI");

            if(go == null)
            {
                Managers.Scene.loadScene -= ClearDictionary;
                Managers.Scene.loadScene += ClearDictionary;

                go = new GameObject { name = "UI" };
            }

            return go.transform;
        }
    }
    public void ShowUI(string uiName)
    {
        if(uiDictionary.TryGetValue(uiName, out GameObject go))
        {
            go.SetActive(true);
        }
        else
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingUI(uiName, true));
        }
    }
    public void HideUI(string uiName)
    {
        if(uiDictionary.TryGetValue(uiName, out GameObject go))
        {
            go.SetActive(false);
        }
    }
    public void CreateUI(string uiName)
    {
        if(!uiDictionary.ContainsKey(uiName))
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingUI(uiName));
        }
    }
    public void DestroyUI(string uiName)
    {
        if(uiDictionary.ContainsKey(uiName))
        {
            Object.Destroy(uiDictionary[uiName]);

            uiDictionary.Remove(uiName);
        }
    }
    public void AddUI()
    {

    }
    public void RemoveUI()
    {

    }
    public async void LoadUI(string path)
    {
        uiDictionary.Add(path, await Util.LoadingToPath<GameObject>(path));
    }
    public IEnumerator CreatingUI(string uiName, bool isActive = false)
    {
        LoadUI(uiName);

        yield return new WaitUntil(() => uiDictionary.ContainsKey(uiName));

        GameObject go = uiDictionary[uiName];

        if (go == null)
        {
            uiDictionary.Remove(uiName);
        }
        else
        {
            uiDictionary[uiName] = GameObject.Instantiate(uiDictionary[uiName], Transform);

            go.SetActive(isActive);
        }
    }
    public void ClearDictionary()
    {
        uiDictionary = new();

        Managers.Scene.loadScene -= ClearDictionary;
    }
}