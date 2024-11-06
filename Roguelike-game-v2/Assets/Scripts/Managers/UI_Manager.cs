using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Manager
{
    public UIElementUtility uiElementUtility = new();

    private Dictionary<string, GameObject> uiDictionary = new();

    private Transform Transform
    {
        get 
        {
            GameObject go = GameObject.Find("UI");

            if(go == null)
            {
                go = new GameObject { name = "UI" };
            }

            return go.transform;
        }
    }
    public string GetName<T>() where T : UserInterface
    {
        string name = typeof(T).ToString();

        name = name.Replace("_UI", "");

        return name;
    }
    public void ShowUI<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out GameObject go))
        {
            go.SetActive(true);
        }
        else
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingUI(name, true));
        }
    }
    public void HideUI<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if (uiDictionary.TryGetValue(name, out GameObject go))
        {
            go.SetActive(false);
        }
    }
    public void CreateUI<T>(bool isActive = false) where T : UserInterface
    {
        string name = GetName<T>();

        if (!uiDictionary.ContainsKey(name))
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingUI(name, isActive));
        }
    }
    public T GetUI<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    public void DestroyUI<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            Object.Destroy(uiDictionary[name]);

            uiDictionary.Remove(name);
        }
    }
    public void AddUI(UserInterface ui)
    {
        string name = ui.GetType().ToString();

        name = name.Replace("_UI", "");

        if(!uiDictionary.ContainsKey(name))
        {
            uiDictionary.Add(name, ui.gameObject);
        }
    }
    public void ClearDictionary()
    {
        uiDictionary = new();
    }
    private async void LoadUI(string path)
    {
        uiDictionary.Add(path, await Util.LoadingToPath<GameObject>(path));
    }
    public IEnumerator CreatingUI(string uiName, bool isActive)
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
            uiDictionary[uiName] = Object.Instantiate(uiDictionary[uiName], Transform);

            go.SetActive(isActive);
        }
    }
}