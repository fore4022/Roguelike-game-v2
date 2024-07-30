using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Test : MonoBehaviour
{
    private List<GameObject> obj;

    private async void Start()
    {
        await Addressables.InitializeAsync().Task;

        obj = await Util.LoadToLable<GameObject>("testLable");
    }
    private void Update()
    {
        Debug.Log(obj[0].name);
        Debug.Log(obj[1].name);
    }
}