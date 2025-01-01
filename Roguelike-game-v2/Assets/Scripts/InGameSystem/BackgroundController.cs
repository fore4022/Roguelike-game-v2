using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BackgroundController : MonoBehaviour
{
    private const float standard = 0.1f;
    private const float width = 17f;
    private const float height = 24;

    private BoxCollider2D col;
    
    private Vector3 increasePos = new();
    private Vector2 direction;
    private Vector2 player;
    private float xPos;
    private float yPos;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("View"))
        {
            return;
        }

        player = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.player.move.Direction;

        xPos = Mathf.Abs(transform.position.x + Mathf.Sign(direction.x) * width);
        yPos = Mathf.Abs(transform.position.y + Mathf.Sign(direction.y) * );

        if (player.x % width < standard)
        {
            increasePos.x = Mathf.Sign(Managers.Game.player.move.Direction.x) * width * 2;
        }
        else
        {
            increasePos.x = 0;
        }

        if(Mathf.Abs(transform.position.y + height) <= Mathf.Abs(player.y))
        {
            increasePos.y = Mathf.Sign(Managers.Game.player.move.Direction.y) * height * 2;
        }
        else
        {
            increasePos.y = 0;
        }

        transform.position += increasePos;

        col.enabled = false;
        col.enabled = true;
    }
}