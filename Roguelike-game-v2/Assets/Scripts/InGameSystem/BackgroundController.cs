using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private Background background;

    private bool isContact = true;

    public Background SetBackground { set { background = value; } }
    public bool IsContact { get { return isContact; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isContact = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isContact = false;

        if(collision.CompareTag("View"))
        {
            background.BackgroundAdjustment();
        }
    }
}