using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class Test_B : MonoBehaviour
{
    public StageInformation_SO stageInfromation;

    private void Start()
    {
        StartCoroutine(DataLoadTest());

        Managers.UserData.UserDataLoad();
    }
    private IEnumerator DataLoadTest()
    {
        while (Managers.UserData.GetUserData == null)
        {
            yield return null;
        }

        Managers.Game.DataLoad(stageInfromation);
    }
}