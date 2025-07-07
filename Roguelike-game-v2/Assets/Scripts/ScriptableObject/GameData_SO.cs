using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "Create New SO/Create New GameData_SO")]
public class GameData_SO : ScriptableObject
{
    [HideInInspector]
    public List<string> stageInfoPathList;

    public List<Stage_SO> stages;

#if UNITY_EDITOR
    private string path;
    private int count { get { return stageInfoPathList.Count; } }

    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(count == stages.Count())
            {
                ValidateUntilReady();
            }
            else
            {
                Validate();
            }
        };
    }
    public void Validate()
    {
        if(count < stages.Count)
        {
            for(int i = count; i < stages.Count; i++)
            {
                path = stages[i].name;

                stageInfoPathList.Add(path);
            }
        }
        else
        {
            stageInfoPathList = new();

            foreach(Stage_SO so in stages)
            {
                path = so.name;

                stageInfoPathList.Add(path);
            }
        }
    }
#endif
}