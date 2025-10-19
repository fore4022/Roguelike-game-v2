using UnityEngine;
public static class CameraUtil
{
    public static float CameraHeight { get { return Camera.main.orthographicSize * 2; } }
    public static float CameraWidth { get { return CameraHeight * Camera.main.aspect; } }
}