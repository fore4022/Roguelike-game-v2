using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnPattern", menuName = "Create New SO/Game Stage/Create New SpawnPattern_SO")]
public class SpawnPattern_SO : ScriptableObject
{
    public List<SpawnPattern_Information> monsterInformation;

    public int duration;
}