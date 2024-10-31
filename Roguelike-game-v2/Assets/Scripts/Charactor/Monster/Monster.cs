using UnityEngine;
public class Monster : RenderableObject
{
    protected Rigidbody2D rigid;

    protected const float spawnRadius = 5;

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        if(render == null)
        {
            render = GetComponent<SpriteRenderer>();

            render.enabled = false;
        }
        
        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();

            rigid.simulated = false;
        }

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
}