using UnityEngine;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(gameObject as Object);

        Debug.Log(gameObject is object);
    }
}