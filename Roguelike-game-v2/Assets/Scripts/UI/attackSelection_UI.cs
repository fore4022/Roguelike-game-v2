using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class attackSelection_UI : MonoBehaviour
{
    [SerializeField]
    private List<(int paddingTop, int spacingY)> gridLayoutValues = new() { (150, 65), (0,0), (0,0)};

    private List<GameObject> attackOptions = new();

    private GridLayoutGroup gridLayoutGroup = null;

    private void Start()
    {
        Init();

        gameObject.SetActive(false);
    }
    private void Init()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        foreach(Transform transform in GetComponentInChildren<Transform>())
        {
            if(transform != this.transform)
            {
                attackOptions.Add(transform.gameObject);

                transform.gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        if(gridLayoutGroup == null)
        {
            return;
        }

        int index = Managers.Game.inGameData.OptionCount - 3;

        AdjustGridLayout(index);

        for(int i = 0; i < Managers.Game.inGameData.OptionCount; i++)
        {
            attackOptions[i].SetActive(true);
        }
    }
    private void AdjustGridLayout(int index)
    {
        gridLayoutGroup.padding.top = gridLayoutValues[index].paddingTop;
        gridLayoutGroup.spacing = new Vector2(0, gridLayoutValues[index].spacingY);
    }
    public void PressButton()//
    {
        this.gameObject.SetActive(false);
    }
}