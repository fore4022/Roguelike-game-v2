using System.Collections;
using UnityEngine;
public class Background : MonoBehaviour
{
    private BackgroundController[] nonContactControllers = new BackgroundController[4];
    private BackgroundController[] controllers = new BackgroundController[4];

    private BackgroundController contactController;
    private Transform controllerTransform;
    private Coroutine adjustment = null;

    private const int width = 20;
    private const int height = 28;

    private Vector3 position;
    private int xValue;
    private int yValue;
    private int index = 0;
    
    private void Start()
    {
        StartCoroutine(Set());
    }
    public void BackgroundAdjustment()
    {
        if (adjustment != null)
        {
            return;
        }

        foreach (BackgroundController controller in controllers)
        {
            if(controller.IsContact)
            {
                nonContactControllers[index] = controller;

                index++;
            }
        }

        if(index == 0)
        {
            return;
        }

        adjustment = StartCoroutine(Adjustment());
    }
    private IEnumerator Adjustment()
    {
        xValue = (int)(Managers.Game.player.gameObject.transform.position.x % width);
        yValue = (int)(Managers.Game.player.gameObject.transform.position.y % height);

        if (xValue != 0)
        {
            xValue = xValue / Mathf.Abs(xValue) * width;
        }

        if(yValue != 0)
        {
            yValue = yValue / Mathf.Abs(yValue) * height;
        }

        yield return null;

        if(index == 1)
        {
            nonContactControllers[0].transform.position = 
        }
        else if(index == 2)
        {
            //
        }

        index = 0;

        adjustment = null;
    }
    private IEnumerator Set()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        controllers = GetComponentsInChildren<BackgroundController>();

        foreach (BackgroundController controller in controllers)
        {
            controller.SetBackground = this;
        }
    }
}