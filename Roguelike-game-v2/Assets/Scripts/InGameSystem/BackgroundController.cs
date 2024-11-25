using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private Background background;
    [SerializeField]
    private bool isContact = false;

    public Background SetBackground { set { background = value; } }
    public bool IsContact { get { return isContact; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("View"))
        {
            isContact = true;

            background?.BackgroundAdjustment(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("View"))
        {
            isContact = false;
        }
    }
}