using UnityEngine;
public class Test_Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, y) * Time.deltaTime * speed;
    }
}