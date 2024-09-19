using UnityEngine;
public class Monster : MonoBehaviour
{
    protected ScriptableObject stat;
    protected SpriteRenderer render;

    protected const float spawnRadius = 5;

    protected virtual void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();

        render.enabled = false;

        Debug.Log("transform position");

        SetPosition();
    }
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);

        float cameraWidth = Util.CameraWidth / 2 + spawnRadius;
        float cameraHeight = Util.CameraHeight / 2 + spawnRadius;

        float x = Mathf.Cos(randomValue) * cameraWidth;
        float y = Mathf.Sin(randomValue) * cameraHeight;

        Debug.Log($"x : {Mathf.Abs(x)}");
        Debug.Log($"camera x : {Util.CameraWidth}");

        Debug.Log($"y : {Mathf.Abs(y)}");
        Debug.Log($"camera y : {Util.CameraHeight}");

        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position;

        Debug.Log("-");

        render.enabled = true;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
    }
}