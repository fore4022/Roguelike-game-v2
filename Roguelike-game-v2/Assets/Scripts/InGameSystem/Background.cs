using System.Collections;
using UnityEngine;
public class Background : MonoBehaviour
{
    private BackgroundController[] nonContactControllers = new BackgroundController[4];
    private BackgroundController[] controllers = new BackgroundController[4];

    private Coroutine adjustment = null;
    private Transform controllerTransform;

    private float width;
    private float height;
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
            if(!controller.IsContact)
            {
                nonContactControllers[index] = controller;

                index++;
            }
        }

        adjustment = StartCoroutine(Adjustment());

        index = 0;
    }
    private IEnumerator Adjustment()
    {
        xValue = (int)(Managers.Game.player.gameObject.transform.position.x % width);
        yValue = (int)(Managers.Game.player.gameObject.transform.position.y % height);

        if(xValue != 0)
        {
            xValue /= Mathf.Abs(xValue);
        }

        if(yValue != 0)
        {
            yValue /= Mathf.Abs(yValue);
        }

        yield return null;

        Debug.Log(index);

        for(int i = 0; i < index; i++)
        {
            controllerTransform = nonContactControllers[i].gameObject.transform;

            switch(i)
            {
                case 0:
                    controllerTransform.position += new Vector3(xValue * width, 0);

                    Debug.Log($"controller-1 position = {xValue * width}, {0}");
                    break;
                case 1:
                    controllerTransform.position += new Vector3(0, yValue * height);
                    Debug.Log($"controller-2 position = {0}, {yValue * height}");
                    break;
                case 2:
                    controllerTransform.position += new Vector3(xValue * width, yValue * height);
                    Debug.Log($"controller-2 position = {xValue * width}, {yValue * height}");
                    break;
            }

            yield return null;
        }

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

        width = controllers[0].gameObject.transform.localScale.x;
        height = controllers[1].gameObject.transform.localScale.y;
    }
}