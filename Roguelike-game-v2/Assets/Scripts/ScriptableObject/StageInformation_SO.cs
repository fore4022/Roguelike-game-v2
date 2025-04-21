using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
#if UNITY_EDITOR
    public List<SpawnInformation_SO> spawnInformationList;

    private string path;
    private int count = 0;

    public void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(count != spawnInformationList.Count)
            {
                Validate();
            }
            else
            {
                ValidateUntilReady();
            }
        };
    }
    public void Validate()
    {
        if(count < spawnInformationList.Count)
        {
            for(int i = count; i < spawnInformationList.Count; i++)
            {
                path = spawnInformationList[i].name;

                spawnInfoPathList.Add(path);
            }

            count = spawnInformationList.Count;
        }
        else
        {
            spawnInfoPathList = new();

            foreach(SpawnInformation_SO so in spawnInformationList)
            {
                path = so.name;

                spawnInfoPathList.Add(path);
            }
        }
    }
#endif

    public List<string> spawnInfoPathList;

    public float difficulty = 1;
    public float statScale = 1;
    public float spawnDelay;
    [Tooltip("Minute")]
    public int requiredTime;
}