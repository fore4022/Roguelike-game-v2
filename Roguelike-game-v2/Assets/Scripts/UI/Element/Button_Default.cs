using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public abstract class Button_Default : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected Button button;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        button.onClick.AddListener(() =>
        {
            PointerClick();
            audioSource.Play();
        });
    }
    protected abstract void PointerClick();
}