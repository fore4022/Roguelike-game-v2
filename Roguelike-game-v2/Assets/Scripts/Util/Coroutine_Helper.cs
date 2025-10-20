using System.Collections;
using UnityEngine;
public static class Coroutine_Helper
{
    private static MonoScript monoScript = null;

    public static void StopCoroutine(Coroutine coroutine)
    {
        GetMonoBehaviour().StopCoroutine(coroutine);
    }
    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return GetMonoBehaviour().StartCoroutine(coroutine);
    }
    private static MonoBehaviour GetMonoBehaviour()
    {
        if(monoScript == null)
        {
            GameObject go = new GameObject("@MonoScript");

            monoScript = go.AddComponent<MonoScript>();

            Object.DontDestroyOnLoad(go);
        }

        return monoScript;
    }
}