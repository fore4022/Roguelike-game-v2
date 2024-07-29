using UnityEngine;
using UnityEngine.AddressableAssets;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Addressables.InitializeAsync();

        GameObject obj = Util.LoadToPath<GameObject>("test").Result;

        Debug.Log(obj);
    }
}
