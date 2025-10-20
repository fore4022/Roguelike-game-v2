using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
/// <summary>
/// <para>
/// UI 로드, 생성, 표시·숨김, 파괴를 통한 관리 기능 제공
/// </para>
/// Addressable과 코루틴을 이용해 UI를 생성하고, 초기화
/// UI 클래스 타입을 통해서 해당하는 UI에 접근
/// </summary>
public class UI_Manager
{
    private Dictionary<string, UserInterface> uiDictionary = new();

    private Transform root;
    
    // 생성된 UI가 위치하는 Root 반환
    private Transform Transform
    {
        get 
        {
            if(root == null)
            {
                GameObject go = GameObject.Find("UI");

                if(go == null)
                {
                    go = new GameObject { name = "UI" };
                }

                root = go.transform;
            }

            return root;
        }
    }
    // 
    public bool IsInitalized()
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
    // 
    public void Show<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(true);
        }
        else
        {
            Coroutine_Helper.StartCoroutine(Creating(name, true));
        }
    }
    // 
    public void Hide<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(false);
        }
    }
    // 
    public void Create<T>(bool isActive = false) where T : UserInterface
    {
        string name = GetName<T>();

        if(!uiDictionary.ContainsKey(name))
        {
            Creating(name, isActive);
        }
    }
    // 
    public T Get<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    // 
    public void Destroy<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            Object.Destroy(uiDictionary[name].gameObject);

            uiDictionary.Remove(name);
        }
    }
    // 
    public void Add(UserInterface ui)
    {
        string name = ui.GetType().ToString().Replace("_UI", "");

        if(!uiDictionary.ContainsKey(name))
        {
            uiDictionary.Add(name, ui);
        }
    }
    // 
    public T ShowAndGet<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            uiDictionary[name].gameObject.SetActive(true);

            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    // 
    public void ClearDictionary()
    {
        uiDictionary = new();
    }
    // 
    private string GetName<T>() where T : UserInterface
    {
        return typeof(T).ToString().Replace("_UI", "");
    }
    // 
    private async Task Load(string uiName)
    {
        GameObject go = await Addressable_Helper.LoadingToPath<GameObject>(uiName);

        if(!uiDictionary.ContainsKey(uiName))
        {
            uiDictionary.Add(uiName, go.GetComponent<UserInterface>());
        }
    }
    // 
    private IEnumerator Creating(string uiName, bool isActive)
    {
        UserInterface ui;
        
        Task loadUI = Load(uiName);

        yield return new WaitUntil(() => loadUI.IsCompleted);

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