using UnityEngine;
[CreateAssetMenu(fileName = "UserLevelInfo", menuName = "Create New SO/User Level/Create New UserLevelInfo_SO")]
public class UserLevelInfo_SO : ScriptableObject
{
    public static int maxLevel = 10;

    public int[] requiredEXP = new int[maxLevel - 1];

    private void OnValidate()
    {
        Util.ResizeArray(ref requiredEXP, maxLevel - 1);
    }
}