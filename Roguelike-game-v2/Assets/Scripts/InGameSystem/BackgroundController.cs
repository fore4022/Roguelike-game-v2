using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private bool isVertical;

    private Background background;

    private bool isContact = true;

    public Background SetBackground { set { background = value; } }
    public bool IsContact { get { return isContact; } }
    public bool Axis { get { return isVertical; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("View"))
        {
            isContact = true;

            background?.BackgroundAdjustment();
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