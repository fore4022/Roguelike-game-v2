using UnityEngine;
public interface IFakeShadowSource
{
    public ShadowMotionType motionType { get; }
    public Sprite CurrentSprite { get; }
    public Vector3 TargetPosition { get; }
    public Vector3 CurrentPosition { get; }
}