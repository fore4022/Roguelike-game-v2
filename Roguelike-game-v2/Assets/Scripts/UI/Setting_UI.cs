public class Setting_UI : NewButton
{
    protected override void PointerClick()
    {
        //show UI
    }
    protected override void Start()
    {
        base.Start();

        childClass = this;
    }
    protected override void Init()
    {
        minScale = 1f;
        maxScale = 1.075f;
        minAlpha = 205;
        maxAlpha = 255;
        duration = 0.1f;
    }
}