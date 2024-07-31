using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Test : MonoBehaviour
{
    private async void Start()
    {
        await ObjectPool.CreateObjects(10, "testLable");
    }
}