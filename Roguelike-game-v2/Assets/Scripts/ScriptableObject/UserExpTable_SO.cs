using UnityEngine;
[CreateAssetMenu(fileName = "UserExpTable", menuName = "Create New SO/Create New UserExpTable_SO")]
public class UserExpTable_SO : ScriptableObject
{
    public static int maxLevel = 10;

    public int[] requiredEXP = new int[maxLevel - 1];

    private void OnValidate()
    {
        Util.ResizeArray(ref requiredEXP, maxLevel - 1);
    }
}