using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private const int width = 18;
    private const int height = 32;
    private const float target_Width = width * 0.75f;
    private const float target_Height = height * 0.75f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("View"))
        {
            return;
        }

        if((int)Managers.Game.player.transform.position.x % target_Width == 0)
        {
            transform.position += new Vector3(Mathf.Sign(Managers.Game.player.move.Direction.x) * width, 0);
        }

        if((int)Managers.Game.player.transform.position.y % target_Height == 0)
        {
            transform.position += new Vector3(0, Mathf.Sign(Managers.Game.player.move.Direction.y) * height);
        }
    }
}
//transform.position += new Vector3(Mathf.Sign(Managers.Game.player.move.Direction.x) * width, 0);
//transform.position += new Vector3(0, Mathf.Sign(Managers.Game.player.move.Direction.y) * height);