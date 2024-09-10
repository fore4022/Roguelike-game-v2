using System.Collections;
using UnityEngine;
public class Test : MonoBehaviour
{
    public StageInformation_SO stageInfromation;
    public UserLevelInfo_SO userLevelInfo;//

    private void Start()
    {
        StartCoroutine(DataLoadTest());

        Managers.UserData.UserDataLoad();
    }
    private IEnumerator DataLoadTest()
    {
        while(Managers.UserData.GetUserData == null)
        {
            yield return null;
        }

        Managers.Game.DataLoad(stageInfromation);
    }
}