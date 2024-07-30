using UnityEngine;
using UnityEngine.AddressableAssets;
public class Test : MonoBehaviour
{
    private GameObject obj;

    private async void Start()
    {
        await Addressables.InitializeAsync().Task;

        obj = await Util.LoadToPath<GameObject>("test");
    }
    private void Update()
    {
        Debug.Log(obj);
    }
}