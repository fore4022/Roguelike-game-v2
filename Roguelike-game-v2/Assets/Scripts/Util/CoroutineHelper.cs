using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// Non-MonoBehaviour 클래스에서도 코루틴을 안전하게 실행·정지할 수 있도록 지원하는 기능을 구현
/// </para>
/// CoroutineHelper로 실행된 코루틴은 CoroutineHelper를 통해서 정지 가능함, 여러 MonoScript로 코루틴을 구분해서 제어 가능
/// </summary>
public static class CoroutineHelper
{
    private static Manage_Mono manage_Mono = null;
    private static UserInterface_Mono userInterface_Mono = null;
    private static InGameSystem_Mono inGameSystem_Mono = null;
    private static Tween_Mono tween_Mono = null;
    private static Etc_Mono etc_Mono = null;

    private static bool isInit = false;

    // 코루틴 실행
    public static Coroutine Start(IEnumerator coroutine, CoroutineType type = CoroutineType.Etc)
    {
        return GetMonoBehaviour(type).StartCoroutine(coroutine);
    }
    // 코루틴 정지
    public static void Stop(Coroutine coroutine, CoroutineType type = CoroutineType.Etc)
    {
        GetMonoBehaviour(type).StopCoroutine(coroutine);
    }
    // Type에 해당하는 MonoScript 코루틴 모두 정지
    public static void StopAllCoroutine(CoroutineType type)
    {
        GetMonoBehaviour(type).StopAllCoroutines();
    }
    // Type에 해당하는 MonoScript를 반환, 초기화되지 않았을 경우에 빈 객체를 생성해 MonoScript 할당 후 반환
    private static MonoBehaviour GetMonoBehaviour(CoroutineType type = CoroutineType.Etc)
    {
        if(!isInit)
        {
            GameObject go = new GameObject("@MonoScript");

            manage_Mono = go.AddComponent<Manage_Mono>();
            userInterface_Mono = go.AddComponent<UserInterface_Mono>();
            inGameSystem_Mono = go.AddComponent<InGameSystem_Mono>();
            tween_Mono = go.AddComponent<Tween_Mono>();
            etc_Mono = go.AddComponent<Etc_Mono>();
            isInit = true;

            Object.DontDestroyOnLoad(go);
        }

        switch(type)
        {
            case CoroutineType.Manage:
                return manage_Mono;
            case CoroutineType.UserInterface:
                return userInterface_Mono;
            case CoroutineType.InGameSystem:
                return inGameSystem_Mono;
            case CoroutineType.Tween:
                return tween_Mono;
        }

        return etc_Mono;
    }
}