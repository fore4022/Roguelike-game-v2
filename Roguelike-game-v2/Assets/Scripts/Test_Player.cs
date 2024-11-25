using UnityEngine;
public class Test_Player : MonoBehaviour
{
    private float speed = 5;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, y) * Time.deltaTime * speed;
    }
}