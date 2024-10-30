using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class Icon : MonoBehaviour
{
    private Image image;

    private Color defaultColor;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        defaultColor = image.color;
    }
    public void UpdateColor(Button button = null)
    {
        if(button != null)
        {
            image.color = button.colors.pressedColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }
}