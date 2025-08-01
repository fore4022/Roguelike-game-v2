using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Background_InGame : MonoBehaviour
{
    private const float width = 4.5f;
    private const float height = 8;
    private const float positionTolerance = 0.25f;

    private Vector3 increasePos = new();
    private Vector2 direction;
    private Vector2 player;
    private float xPos;
    private float yPos;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("View") || Managers.Game.player == null)
        {
            return;
        }

        increasePos = new();
        direction = Managers.Game.player.move.Direction;
        player = Managers.Game.player.gameObject.transform.position;
        xPos = Mathf.Round(player.x * 10) / 10;
        yPos = Mathf.Round(player.y * 10) / 10;

        if(Mathf.Abs(Mathf.Abs(xPos - transform.localPosition.x) - width * 5) < positionTolerance)
        {
            increasePos.x += Mathf.Sign(direction.x) * width * 8;
        }

        if(Mathf.Abs(Mathf.Abs(yPos - transform.localPosition.y) - height * 5) < positionTolerance)
        {
            increasePos.y += Mathf.Sign(direction.y) * height * 8;
        }

        transform.position += increasePos;
    }
} 