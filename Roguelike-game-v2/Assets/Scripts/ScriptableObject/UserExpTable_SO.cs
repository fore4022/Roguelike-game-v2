using UnityEngine;
[CreateAssetMenu(fileName = "UserExpTable", menuName = "Create New SO/Create New UserExpTable_SO")]
public class UserExpTable_SO : ScriptableObject
{
    public static int maxLevel = 10;

    public int[] requiredEXP = new int[maxLevel - 1];

    // ���� ����ġ ǥ�� �׸� ���� maxLevel�� ������ ����
    private void OnValidate()
    {
        ArrayUtil.ResizeArray(ref requiredEXP, maxLevel - 1);
    }
}