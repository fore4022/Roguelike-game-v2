using UnityEngine;
public class SkillRangeVisualizer : MonoBehaviour
{
    private SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        render.material.SetColor("Outline Color Base", Color.blue);
    }
}