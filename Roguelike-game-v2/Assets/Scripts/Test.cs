using UnityEngine;
public class Test : MonoBehaviour
{
    public StageInformation_SO stageInfromation;

    private void Start()
    {
        Managers.Game.DataLoad(stageInfromation);
    }
}