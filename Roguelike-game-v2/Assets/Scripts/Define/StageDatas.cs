using System.Collections.Generic;
using System.Linq;
/// <summary>
/// <para>
/// 모든 스테이지를 담는 타입
/// </para>
/// 이어지는 스테이지를 반환
/// </summary>
[System.Serializable]
public class StageDatas
{
    private List<Stage_SO> icons;
    private StageDatas_SO so;

    public StageDatas_SO SO
    {
        set
        {
            so = value;
            icons = so.stages;
        }
    }
    public List<Stage_SO> Icons { get { return icons; } }
    public Stage_SO GetSO(string stageName, int sign)
    {
        int index = 0;

        for(int i = 0; i < icons.Count(); i++)
        {
            if(icons[i].stagePath == stageName)
            {
                index = i + sign;

                break;
            }
        }

        if(index == icons.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = icons.Count() - 1;
        }

        Managers.Data.user.StageName = icons[index].stagePath;

        return icons[index];
    }
}