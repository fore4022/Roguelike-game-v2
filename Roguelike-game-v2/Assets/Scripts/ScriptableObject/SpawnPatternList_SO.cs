using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnPatternList", menuName = "Create New SO/Game Stage/Create New SpawnPatternList_SO")]
public class SpawnPatternList_SO : ScriptableObject
{
    public List<SpawnPattern_SO> patterns;
}