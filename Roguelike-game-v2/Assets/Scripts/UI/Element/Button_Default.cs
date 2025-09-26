using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public abstract class Button_Default : UserInterface
{
    [SerializeField]
    protected AudioSource audioSource;

    protected RectTransform rectTransform;
    protected Button button;

    public override void SetUserInterface()
    {
        Init();
    }
    protected virtual void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        AddButtonEvents();
    }
    protected virtual void AddButtonEvents()
    {
        button.onClick.AddListener(() =>
        {
            PointerClick();
            audioSource.Play();
        });
    }
    protected abstract void PointerClick();
}