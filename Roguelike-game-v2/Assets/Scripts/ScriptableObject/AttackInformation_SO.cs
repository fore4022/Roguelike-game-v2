using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackInformation", menuName = "Create New SO/Create New AttackInformation_SO")]
public class AttackInformation_SO : ScriptableObject
{
    public AnimatorController controller;
    public GameObject skillObject;
    public Sprite sprite;

    public string attackType;
    public string explanation;
}