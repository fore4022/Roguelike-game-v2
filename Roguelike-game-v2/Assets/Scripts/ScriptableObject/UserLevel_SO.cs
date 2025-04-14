using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/Create New UserLevel_SO")]
public class UserLevel_SO : ScriptableObject
{
    [HideInInspector]
    public List<string> pathList;

#if UNITY_EDITOR

    public List<AttackInformation_SO> attackInformationList;

    private string path;
    private int count = 0;

    private void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if (count == attackInformationList.Count)
            {
                ValidateUntilReady();
            }

            Validate();
        };
    }
    private void Validate()
    {
        if (count < attackInformationList.Count)
        {
            for (int i = count; i < attackInformationList.Count; i++)
            {
                path = $"Assets/SO/AttackInformation/{attackInformationList[i].name}.asset";

                pathList.Add(path);
            }

            count = attackInformationList.Count;
        }
        else
        {
            pathList = new();

            foreach (AttackInformation_SO so in attackInformationList)
            {
                path = $"Assets/SO/AttackInformation/{so.name}.asset";

                pathList.Add(path);
            }
        }
    }
#endif
}