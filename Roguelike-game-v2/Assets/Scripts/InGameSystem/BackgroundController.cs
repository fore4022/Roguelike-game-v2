using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private const int width = 9;
    private const int height = 16;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("View"))
        {
            return;
        }

        if(transform.position.x != Mathf.Round(Managers.Game.player.transform.position.x))
        {
            if (Mathf.Round(Managers.Game.player.transform.position.x) % width == 0)
            {
                transform.position += new Vector3(Mathf.Sign(Managers.Game.player.move.Direction.x) * width * 2, 0);
            }
        }

        if(transform.position.y != Mathf.Round(Managers.Game.player.transform.position.y))
        {
            if (Mathf.Round(Managers.Game.player.transform.position.y) % height == 0)
            {
                transform.position += new Vector3(0, Mathf.Sign(Managers.Game.player.move.Direction.y) * height * 2);
            }
        }
    }
}