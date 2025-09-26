using UnityEngine;
using UnityEngine.UI;
public class StageInformationButton_UI : Button_Default
{
    [SerializeField]
    private Color visible;
    [SerializeField]
    private Color invisible;

    private Image image;

    private bool isVisible = false;

    protected override void Init()
    {
        image = GetComponent<Image>();

        base.Init();
    }
    protected override void PointerClick()
    {
        isVisible = !isVisible;

        InformationUpdate();
    }
    public void InformationUpdate()
    {
        if(isVisible)
        {
            Show_StageInformation();
        }
        else
        {
            Hide_StageInformation();
        }
    }
    public void Show_StageInformation()
    {
        image.color = visible;
    }
    public void Hide_StageInformation()
    {
        image.color = invisible;
    }
}