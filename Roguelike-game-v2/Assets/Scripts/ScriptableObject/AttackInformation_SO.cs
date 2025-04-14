using UnityEngine;
[CreateAssetMenu(fileName = "AttackInformation", menuName = "Create New SO/Create New AttackInformation_SO")]
public class AttackInformation_SO : ScriptableObject
{
    public RuntimeAnimatorController controller;
    public GameObject attack;
    public Sprite sprite;

    public string attackType;
    public string explanation;
}