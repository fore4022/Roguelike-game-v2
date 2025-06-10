using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class UI_Manager
{
    private Dictionary<string, UserInterface> uiDictionary = new();
    
    public bool IsInitalized
    {
        get
        {
            foreach(UserInterface ui in uiDictionary.Values)
            {
                if(!ui.IsInitalized)
                {
                    ui.SetUI();

                    return false;
                }
            }

            return true;
        }
    }
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
    public void Show<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(true);
        }
        else
        {
            Creating(name, true);
        }
    }
    public void Hide<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(false);
        }
    }
    public void Create<T>(bool isActive = false) where T : UserInterface
    {
        string name = GetName<T>();

        if(!uiDictionary.ContainsKey(name))
        {
            Creating(name, isActive);
        }
    }
    public T Get<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    public void Destroy<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            Object.Destroy(uiDictionary[name].gameObject);

            uiDictionary.Remove(name);
        }
    }
    public void Add(UserInterface ui)
    {
        string name = ui.GetType().ToString();

        name = name.Replace("_UI", "");

        if(!uiDictionary.ContainsKey(name))
        {
            uiDictionary.Add(name, ui);
        }
    }
    public T ShowAndGet<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if (uiDictionary.ContainsKey(name))
        {
            uiDictionary[name].gameObject.SetActive(true);

            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    public void ClearDictionary()
    {
        uiDictionary = new();
    }
    private string GetName<T>() where T : UserInterface
    {
        string name = typeof(T).ToString();

        name = name.Replace("_UI", "");

        return name;
    }
    private void Load(string uiName)
    {
        GameObject go = Util.LoadingToPath<GameObject>(uiName);

        if(!uiDictionary.ContainsKey(uiName))
        {
            uiDictionary.Add(uiName, go.GetComponent<UserInterface>());
        }
    }
    private void Creating(string uiName, bool isActive)
    {
        UserInterface ui;
        
        Load(uiName);

        ui = uiDictionary[uiName];

        if(ui == null)
        {
            uiDictionary.Remove(uiName);
        }
        else
        {
            ui = uiDictionary[uiName] = Object.Instantiate(uiDictionary[uiName], Transform);

            ui.gameObject.SetActive(isActive);

            if(isActive)
            {
                ui.SetUI();
            }
        }
    }
}