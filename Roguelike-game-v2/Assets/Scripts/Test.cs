using UnityEngine;
public class Test : MonoBehaviour
{
    public FileReference file1;
    public FileReference file2;

    private PlayerStat_Mono stat;

    private void Start()
    {
        file1.targetObject = stat;
        file2.targetObject = stat;
    }
}