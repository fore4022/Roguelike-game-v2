using UnityEngine;
public class attackSelection_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject attackOptionPanel;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        for(int i = 0; i < Managers.Game.inGameData.OptionCount; i++)
        {
            
        }
    }
}