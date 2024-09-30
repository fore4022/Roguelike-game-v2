using UnityEngine;
[CreateAssetMenu(fileName = "AttackInformation", menuName = "Create New SO/Create New AttackInformation_SO")]
public class AttackInformation_SO : ScriptableObject
{
    public GameObject skillObject;

    public string attackName;
    public string explanation;
    public float coolTime;
}