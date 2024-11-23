using System.Collections;
using UnityEngine;
public class Background : MonoBehaviour
{
    private BackgroundController[] nonContactControllers = new BackgroundController[4];
    private BackgroundController[] controllers = new BackgroundController[4];

    private Coroutine adjustment = null;
    private Transform controllerTransform;

    private const int width = 20;
    private const int height = 28;

    private Vector3 position;
    private int index = 0;
    private int xValue;
    private int yValue;
    
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
            xValue = xValue / Mathf.Abs(xValue) * width * 2;
        }

        if(yValue != 0)
        {
            yValue = yValue / Mathf.Abs(yValue) * height * 2;
        }

        yield return null;

        //if(index == 1)
        //{
        //    for(int i = 0; i < index; i++)
        //    {
        //        controllerTransform = nonContactControllers[i].gameObject.transform;

        //        if(i == 0)
        //        {
        //            controllerTransform.position += new Vector3(xValue, 0);
        //        }
        //        else if(i == 1)
        //        {
        //            controllerTransform.position += new Vector3(0, yValue);
        //        }
        //        else if(i == 2)
        //        {
        //            controllerTransform.position += new Vector3(xValue, yValue);
        //        }
        //    }
        //}
        //else if(index == 2)
        //{
        //    if(xValue > yValue)
        //    {
        //        position = new Vector3(xValue, 0);
        //    }
        //    else
        //    {
        //        position = new Vector3(0, yValue);
        //    }

        //    for(int i = 0; i < index; i++)
        //    {
        //        nonContactControllers[i].gameObject.transform.position += position;
        //    }
        //}

        if(index == 1)
        {
            
        }
        else if(index == 2)
        {

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