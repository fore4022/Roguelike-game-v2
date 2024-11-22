using UnityEngine;
public class BackgroundController : MonoBehaviour
{
    private Background background;

    private bool isContact = true;

    public Background SetBackground { set { background = value; } }
    public bool IsContact { get { return isContact; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("View"))
        {
            Debug.Log("is trigger false");

            isContact = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isContact = true;

        if(collision.CompareTag("View"))
        {
            Debug.Log("Start adjustment");

            background?.BackgroundAdjustment();
        }
    }
}