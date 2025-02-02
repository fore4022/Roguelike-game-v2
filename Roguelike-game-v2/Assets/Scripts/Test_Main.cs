using System.Collections;
using UnityEngine;
public class Test_Main : MonoBehaviour
{
    public StageInformation_SO stageInfromation;//

    private void Start()
    {
        StartCoroutine(DataLoadTest());

        Managers.UserData.UserDataLoad();
    }
    private IEnumerator DataLoadTest()
    {
        while (Managers.UserData.userData == null)
        {
            yield return null;
        }

        Managers.Game.DataLoad();
    }
}