using UnityEngine;
public class Test_InGame : MonoBehaviour
{
    [SerializeField]
    private attackSelection_UI attackSelectionUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attackSelectionUI.Set();
        }
    }
}