using UnityEngine;
public class InGameDataManage
{
    private const int maxOptionCount = 5;

    private int defaultOptionCount = 3;
    
    public int OptionCount { get { return Mathf.Min(defaultOptionCount, maxOptionCount); } }//
}