using System.Collections;
using UnityEngine;
public class Background : MonoBehaviour
{
    private BackgroundController[] nonContactControllers = new BackgroundController[4];
    private BackgroundController[] controllers = new BackgroundController[4];

    private WaitForSeconds delay = new(0.01f);
    private Coroutine adjustment = null;

    private const int width = 20;
    private const int height = 28;
     
    private int xValue;
    private int yValue;
    private int xPos;
    private int yPos;
    private int index = 0;
    
    private void Start()
    {
        StartCoroutine(Set());
    }
    public void BackgroundAdjustment(BackgroundController contactController)
    {
        if (adjustment != null)
        {
            return;
        }

        Debug.Log(contactController.gameObject.name);

        foreach (BackgroundController controller in controllers)
        {
            if(!controller.IsContact)
            {
                nonContactControllers[index] = controller;

                index++;
            }
        }

        if(index == 0)
        {
            return;
        }

        adjustment = StartCoroutine(Adjustment(contactController.gameObject.transform.position));
    }
    private IEnumerator Adjustment(Vector3 position)
    {
        xValue = (int)(Managers.Game.player.gameObject.transform.position.x % width);
        yValue = (int)(Managers.Game.player.gameObject.transform.position.y % height);

        if (Mathf.Sign(xValue) != 0)
        {
            xPos = (int)Mathf.Sign(xValue) * width;
        }
        else
        {
            xPos = width;
        }

        if(Mathf.Sign(yValue) != 0)
        {
            yPos = (int)Mathf.Sign(yValue) * height;
        }
        else
        {
            yPos = height;
        }

        yield return null;

        if(index == 3)
        {
            nonContactControllers[0].transform.position = position + new Vector3(xPos, 0);
            nonContactControllers[1].transform.position = position + new Vector3(0, yPos);
            nonContactControllers[2].transform.position = position + new Vector3(xPos, yPos);
        }
        //else if(index == 2)
        //{
        //    if(Mathf.Abs(xValue) > Mathf.Abs(yValue))
        //    {
        //        nonContactControllers[0].transform.position = position + new Vector3(xPos, 0);
        //        nonContactControllers[1].transform.position = position + new Vector3(xPos, yPos);
        //    }
        //    else
        //    {
        //        nonContactControllers[0].transform.position = position + new Vector3(0, yPos);
        //        nonContactControllers[1].transform.position = position + new Vector3(xPos, yPos);
        //    }
        //}

        index = 0;

        yield return delay;

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