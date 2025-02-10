using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackInformation", menuName = "Create New SO/Create New AttackInformation_SO")]
public class AttackInformation_SO : ScriptableObject
{
    public GameObject skillObject;
    public Sprite sprite;
    public AnimatorController controller;

    public string attackType;
    public string explanation;
}