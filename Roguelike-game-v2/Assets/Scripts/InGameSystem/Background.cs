using UnityEngine;
public class Background : MonoBehaviour
{
    BackgroundController[] controllers = new BackgroundController[4];

    private void Start()
    {
        controllers = GetComponentsInChildren<BackgroundController>();

        foreach(BackgroundController controller in controllers)
        {
            controller.SetBackground = this;
        }
    }
    public void BackgroundAdjustment()
    {
        foreach(BackgroundController controller in controllers)
        {
            if(!controller.IsContact)
            {
                Adjustment(controller.gameObject);
            }
        }
    }
    private void Adjustment(GameObject go)
    {
        //adjustment background position
    }
}