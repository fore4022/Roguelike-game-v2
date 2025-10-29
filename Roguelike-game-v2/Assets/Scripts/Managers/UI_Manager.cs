using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
/// <summary>
/// <para>
/// UI �ε�, ����, ǥ�á�����, �ı��� ���� ���� ��� ����
/// </para>
/// Addressable�� �ڷ�ƾ�� �̿��� UI�� �����ϰ�, �ʱ�ȭ
/// UI Ŭ���� Ÿ���� ���ؼ� �ش��ϴ� UI�� ����
/// </summary>
public class UI_Manager
{
    private Dictionary<string, UserInterface> uiDictionary = new();

    private Transform root;
    
    // ������ UI�� ��ġ�ϴ� Root ��ȯ
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
    // ��� UI�� �ʱ�ȭ ���θ� Ȯ��, �ʱ�ȭ���� ���� UI�� �ʱ�ȭ�� ����
    public bool IsInitalized()
    {
        foreach(UserInterface ui in uiDictionary.Values)
        {
            if(!ui.IsInitalized)
            {
                ui.InitUI();

                return false;
            }
        }

        return true;
    }
    // Ÿ�Կ� �ش��ϴ� UI�� Ȱ��ȭ, �ش��ϴ� UI�� ���ٸ� ������ ���� Ȱ��ȭ
    public void Show<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(true);
        }
        else
        {
            CoroutineHelper.Start(Creating(name, true));
        }
    }
    // Ÿ�Կ� �ش��ϴ� UI�� ��Ȱ��ȭ
    public void Hide<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(false);
        }
    }
    // Ÿ�Կ� �ش��ϴ� UI�� �������� ���� ��� ����, ���� ���� Ȱ��ȭ ���� ���� ����
    public void Create<T>(bool isActive = false) where T : UserInterface
    {
        string name = GetName<T>();

        if(!uiDictionary.ContainsKey(name))
        {
            Creating(name, isActive);
        }
    }
    // Ÿ�Կ� �ش��ϴ� UI �ν��Ͻ��� ��ȯ
    public T Get<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            return uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    // Ÿ�Կ� �ش��ϴ� UI�� �����Ѵٸ� ����
    public void Destroy<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(uiDictionary.ContainsKey(name))
        {
            Object.Destroy(uiDictionary[name].gameObject);

            uiDictionary.Remove(name);
        }
    }
    // UI ���
    public void Add(UserInterface ui)
    {
        string name = ui.GetType().ToString().Replace("_UI", "");

        if(!uiDictionary.ContainsKey(name))
        {
            uiDictionary.Add(name, ui);
        }
    }
    // Ÿ�Կ� �ش��ϴ� UI Ȱ��ȭ �� �ν��Ͻ� ��ȯ
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
    // UI ��� �ʱ�ȭ
    public void ClearDictionary()
    {
        uiDictionary = new();
    }
    // Ÿ���� ���ؼ� UI ��ųʸ� Ű ��ȯ
    private string GetName<T>() where T : UserInterface
    {
        return typeof(T).ToString().Replace("_UI", "");
    }
    // Addressable�� UI ������ �ҷ�����
    private async Task LoadUI(string uiName)
    {
        GameObject go = await AddressableHelper.LoadingToPath<GameObject>(uiName);

        if(!uiDictionary.ContainsKey(uiName))
        {
            uiDictionary.Add(uiName, go.GetComponent<UserInterface>());
        }
    }
    // �Է¹��� Ű�� UI �ε� ��� ���� ���� �� Ȱ��ȭ ���� ����
    private IEnumerator Creating(string uiName, bool isActive)
    {
        UserInterface ui;
        
        Task loadUI = LoadUI(uiName);

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
                ui.InitUI();
            }
        }
    }
}