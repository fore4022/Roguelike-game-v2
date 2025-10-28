using System.Collections;
using UnityEngine;
public class MonsterInitializer : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        yield return null;
    }
}