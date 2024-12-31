using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private const float width = 4.5f;
    private const float height = 8;

    private float xPos;
    private float yPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("View"))
        {
            return;
        }

        xPos = Mathf.Round(Managers.Game.player.transform.position.x * 10) / 10;
        yPos = Mathf.Round(Managers.Game.player.transform.position.y * 10) / 10;

        if(transform.position.x != xPos)
        {
            if (xPos % width == 0)
            {
                transform.position += new Vector3(Mathf.Sign(Managers.Game.player.move.Direction.x) * width * 2, 0);
            }
        }

        if(transform.position.y != yPos)
        {
            if(yPos % height == 0)
            {
                transform.position += new Vector3(0, Mathf.Sign(Managers.Game.player.move.Direction.y) * height * 2);
            }
        }
    }
}