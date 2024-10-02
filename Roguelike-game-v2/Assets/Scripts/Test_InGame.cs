using UnityEngine;
public class Test_InGame : MonoBehaviour
{
    [SerializeField]
    private GameObject attackSelectionUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attackSelectionUI.SetActive(true);
        }
    }
}