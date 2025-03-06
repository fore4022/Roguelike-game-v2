using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Background_InGame : MonoBehaviour
{
    private const float width = 4.5f;
    private const float height = 8;

    private Vector3 increasePos = new();
    private Vector2 direction;
    private Vector2 player;
    private float xPos;
    private float yPos;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Managers.Game.player == null)
        {
            return;
        }

        if(!collision.gameObject.CompareTag("View"))
        {
            return;
        }

        player = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.player.move.Direction;
        xPos = Mathf.Round(Mathf.Abs(player.x) * 10) / 10;
        yPos = Mathf.Round(Mathf.Abs(player.y));

        increasePos = new();

        if(xPos % width < 0.25f)
        {
            if(Mathf.Abs((int)(xPos / width) % 2) == 1)
            {
                increasePos.x += Mathf.Sign(direction.x) * width * 8;
            }
        }

        if(yPos % height < 0.25f)
        {
            if (Mathf.Abs((int)(yPos / height) % 2) == 1)
            {
                increasePos.y += Mathf.Sign(direction.y) * height * 8;
            }
        }

        transform.position += increasePos;
    }
}