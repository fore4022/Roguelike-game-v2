using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    public List<GameObject> monsterList;

    public Sprite background;

    public string stageName;
    public float difficulty;
    public float spawnDelay;
}