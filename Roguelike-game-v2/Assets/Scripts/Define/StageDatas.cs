using System.Collections.Generic;
using System.Linq;
/// <summary>
/// <para>
/// ��� ���������� ��� Ÿ��
/// </para>
/// �̾����� ���������� ��ȯ�Ѵ�.
/// </summary>
[System.Serializable]
public class StageDatas
{
    private List<Stage_SO> stages;
    private StageDatas_SO so;

    public StageDatas_SO SO
    {
        set
        {
            so = value;
            stages = so.stages;
        }
    }
    public List<Stage_SO> Stages { get { return stages; } }
    public Stage_SO GetStageSO(string stageName, int sign)
    {
        int index = 0;

        for(int i = 0; i < stages.Count(); i++)
        {
            if(stages[i].stagePath == stageName)
            {
                index = i + sign;

                break;
            }
        }

        if(index == stages.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = stages.Count() - 1;
        }

        Managers.UserData.data.StageName = stages[index].stagePath;

        return stages[index];
    }
}