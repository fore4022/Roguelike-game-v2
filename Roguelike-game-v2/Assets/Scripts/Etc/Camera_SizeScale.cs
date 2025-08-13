using UnityEngine;
public class Camera_SizeScale : MonoBehaviour
{
    public static readonly float deviceScale = (float)Screen.width / Screen.height;
    
    private InGame_Camera inGameCamera;

    private static bool isDeviceScaleSmaller;

    private const float defaultScale = (float)1080 / 1920;

    public static float OrthographicSizeScale { get { return isDeviceScaleSmaller ? defaultScale / deviceScale : 1; } }
    private void Awake()
    {
        inGameCamera = GetComponent<InGame_Camera>();

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Managers.Scene.loadComplete += OrthographicsSizeUpdate;

        isDeviceScaleSmaller = deviceScale <= defaultScale;

        OrthographicsSizeUpdate();
    }
    public void OrthographicsSizeUpdate()
    {
        switch(Managers.Scene.CurrentSceneName)
        {
            case "Title":
            case "Main":
                transform.position = new(0, 0, -10);
                inGameCamera.enabled = false;

                ResizeOrthographicSize(CameraSizes.Common);
                break;
            case "InGame":
                if(!inGameCamera.enabled)
                {
                    inGameCamera.enabled = true;
                }

                ResizeOrthographicSize(CameraSizes.inGame);
                break;
        }
    }
    private void ResizeOrthographicSize(float size)
    {
        Camera.main.orthographicSize = size * (defaultScale / deviceScale);
    }
}