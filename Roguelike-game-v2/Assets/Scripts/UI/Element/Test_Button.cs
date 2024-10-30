public class Test_Button : NewButton
{
    protected override void PointerClick()
    {
        UnityEngine.Debug.Log("success");
    }
    protected override void Init()
    {
        minScale = 1f;
        maxScale = 1.035f;
        minAlpha = 155;
        maxAlpha = 235;
        duration = 0.1f;
    }
    protected override void Start()
    {
        childClass = this;

        base.Start();
    }
}