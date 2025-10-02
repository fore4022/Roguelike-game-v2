using TMPro;
using UnityEngine;
public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.enabled = false;

        gameObject.SetActive(false);
    }
    public void SetInformation(int damage, Vector3 position)
    {
        transform.position = position;
        text.text = $"{damage:N0}";
        text.enabled = true;

        UIElementUtility.SetTextAlpha(text, 255);
    }
}