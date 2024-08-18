using UnityEngine;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Object obj = new GameObject();

        var Type = obj as Object;

        Debug.Log((obj as Object).GetType());

        Debug.Log(obj is object);
    }
}