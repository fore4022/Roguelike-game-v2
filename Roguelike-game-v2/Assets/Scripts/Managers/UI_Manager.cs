using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
public class UI_Manager
{
    public UIElementUtility uiElementUtility = new();

    private Dictionary<string, UserInterface> uiDictionary = new();

    private bool isInitalize;

    public bool IsInitalize { get { return isInitalize; } }
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

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(true);
        }
        else
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingUI(name, true));
        }
    }
    public void HideUI<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if (uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(false);
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
            Object.Destroy(uiDictionary[name].gameObject);

            uiDictionary.Remove(name);
        }
    }
    public void AddUI(UserInterface ui)
    {
        string name = ui.GetType().ToString();

        name = name.Replace("_UI", "");

        if(!uiDictionary.ContainsKey(name))
        {
            uiDictionary.Add(name, ui);

            isInitalize = false;
        }
    }
    public void CreateAndExcute<T>(Action action) where T : UserInterface// type?
    {
        Delegate[] delegates = action.GetInvocationList();

        string typeName = GetName<T>();

        Util.GetMonoBehaviour().StartCoroutine(CreatingUI(typeName, true));
        Util.GetMonoBehaviour().StartCoroutine(Executing(typeName, delegates));
    }
    public void ClearDictionary()
    {
        uiDictionary = new();
    }
    private async void LoadUI(string path)
    {
        GameObject go = await Util.LoadingToPath<GameObject>(path);

        uiDictionary.Add(path, go.GetComponent<UserInterface>());
    }
    public IEnumerator CreatingUI(string uiName, bool isActive)
    {
        LoadUI(uiName);

        yield return new WaitUntil(() => uiDictionary.ContainsKey(uiName));

        UserInterface ui = uiDictionary[uiName];

        if (ui == null)
        {
            uiDictionary.Remove(uiName);
        }
        else
        {
            uiDictionary[uiName] = Object.Instantiate(uiDictionary[uiName], Transform);

            ui.gameObject.SetActive(isActive);
        }
    }
    private IEnumerator Executing(string typeName, Delegate[] methods)
    {
        yield return new WaitUntil(() => uiDictionary != null);

        yield return new WaitUntil(() => uiDictionary.ContainsKey(typeName));

        Type type = uiDictionary[typeName].GetType();

        foreach(Delegate _method in methods)
        {
            MethodInfo method = type.GetMethod(_method.ToString());

            method.Invoke(type, null);
        }
    }
}