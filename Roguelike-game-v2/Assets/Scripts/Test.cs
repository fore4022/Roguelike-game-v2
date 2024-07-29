using UnityEngine;
using UnityEngine.AddressableAssets;
public class Test : Util
{
    private GameObject obj;
    private void Start()
    {
        Init();
    }
    private async void Init()
    {
        await Addressables.InitializeAsync().Task;

        obj = LoadToPath<GameObject>("test").Result;
    }
    private void Update()
    {
        Debug.Log(obj);
    }
}