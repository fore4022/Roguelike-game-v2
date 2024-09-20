using UnityEngine;
public class Monster : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected SpriteRenderer render;

    protected const float spawnRadius = 5;

    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        render.enabled = false;
        rigid.simulated = false;

        SetPosition();
    }
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);

        float cameraWidth = Util.CameraWidth / 2 + spawnRadius;
        float cameraHeight = Util.CameraHeight / 2 + spawnRadius;

        float x = Mathf.Cos(randomValue) * cameraWidth;
        float y = Mathf.Sin(randomValue) * cameraHeight;

        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position;

        render.enabled = true;
        rigid.simulated = true;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
    }
}