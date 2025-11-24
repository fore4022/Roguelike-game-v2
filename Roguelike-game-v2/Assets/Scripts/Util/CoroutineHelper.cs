using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// Non-MonoBehaviour 클래스에서도 코루틴을 안전하게 실행·정지할 수 있도록 지원하는 기능을 구현
/// </para>
/// CoroutineHelper로 실행된 코루틴은 CoroutineHelper를 통해서 정지 가능함
/// </summary>
public static class CoroutineHelper
{
    private static MonoScript monoScript = null;
    private static SkillCast_Mono skillCast_Mono = null;
    private static UserInterface_Mono userInterface_Mono = null;
    private static Etc_Mono etc_Mono = null;

    // 코루틴 실행
    public static Coroutine Start(IEnumerator coroutine)
    {
        return GetMonoBehaviour().StartCoroutine(coroutine);
    }
    // 코루틴 정지
    public static void Stop(Coroutine coroutine)
    {
        GetMonoBehaviour().StopCoroutine(coroutine);
    }
    // monoScript를 반환, monoScript가 null일 경우에 빈 객체를 생성해 MonoScript 할당 후 반환
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