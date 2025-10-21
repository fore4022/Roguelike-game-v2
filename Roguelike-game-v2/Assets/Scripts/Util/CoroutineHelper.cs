using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// Non-MonoBehaviour Ŭ���������� �ڷ�ƾ�� �����ϰ� ���ࡤ������ �� �ֵ��� �����ϴ� ����� ����
/// </para>
/// CoroutineHelper�� ����� �ڷ�ƾ�� CoroutineHelper�θ� ������
/// </summary>
public static class CoroutineHelper
{
    private static MonoScript monoScript = null;

    // �ڷ�ƾ ����
    public static void StopCoroutine(Coroutine coroutine)
    {
        GetMonoBehaviour().StopCoroutine(coroutine);
    }
    // �ڷ�ƾ ����
    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return GetMonoBehaviour().StartCoroutine(coroutine);
    }
    // monoScript�� ��ȯ, monoScript�� null�� ��쿡 �� ��ü�� ������ MonoScript �Ҵ� �� ��ȯ
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