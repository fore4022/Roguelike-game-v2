using UnityEngine;
public class Background_Menu : MonoBehaviour
{
    private Renderer render;

    private Vector2 offset = new();
    private Vector2 vec = new();
    private float speed = 0.0025f;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }
    private void Start()
    {
        vec = new Vector2(speed * Time.fixedDeltaTime, 0);
    }
    private void FixedUpdate()
    {
        render.material.SetTextureOffset("_MainTex", offset);

        offset += vec;
    }
}