using UnityEngine;
[CreateAssetMenu(fileName = "UserLevelData", menuName = "Create New SO/User Level/Create New UserLevelData_SO")]
public class UserLevelData_SO : ScriptableObject
{
    public static int maxLevel = 10;

    public int[] requiredEXP = new int[maxLevel - 1];

    private void OnValidate()
    {
        Util.ResizeArray(ref requiredEXP, maxLevel - 1);
    }
}