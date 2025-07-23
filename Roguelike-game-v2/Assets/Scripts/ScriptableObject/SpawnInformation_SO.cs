using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnInformation", menuName = "Create New SO/Game Stage/Create New SpawnInformation_SO")]
public class SpawnInformation_SO : ScriptableObject
{
    public List<SpawnInformation> monsterInformation;

    public int duration;
}