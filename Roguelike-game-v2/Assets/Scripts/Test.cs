using System.Collections;
using UnityEngine;
public class Test : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("InvokeRepeating", 1, 1);

        Invoke("StopInvokeRepeating", 5);
    }
    private void InvokeRepeating()
    {
        Debug.Log("~ing");
    }
    private void StopInvokeRepeating()
    {
        CancelInvoke("InvokeRepeating");
    }
}
