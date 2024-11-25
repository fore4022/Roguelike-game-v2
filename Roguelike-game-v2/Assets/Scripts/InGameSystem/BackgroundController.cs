using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private const int width = 20;
    private const int height = 28;
    private const int half_Width = width / 2;
    private const int half_Height = height / 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("View"))
        {
            return;
        }

        transform.position += new Vector3(Mathf.Sign(Managers.Game.player.move.Direction.x) * half_Width, 0);

        transform.position += new Vector3(0, Mathf.Sign(Managers.Game.player.move.Direction.y) * half_Height);
    }
}