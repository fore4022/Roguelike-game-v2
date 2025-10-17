using System.Collections;
using UnityEngine;
/// <summary>
/// ���� Scene �ʱ�ȭ
/// </summary>
public class Main_Scene : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        int levelUpCount = 0;

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        while(Managers.Data.user.Exp >= Managers.Data.UserExpTable.requiredEXP[Managers.Data.user.Level - 1])
        {
            Managers.Data.user.Exp -= Managers.Data.UserExpTable.requiredEXP[Managers.Data.user.Level - 1];
            Managers.Data.user.Level++;

            levelUpCount++;

            if(Managers.Data.user.Level == UserExpTable_SO.maxLevel)
            {
                break;
            }
        }

        if(levelUpCount != 0)
        {
            Managers.Data.user.StatPoint += levelUpCount;

            Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
            Managers.UI.Get<UserExpSlider_UI>().UpdateExp();
            Managers.UI.Get<UserLevelText_UI>().LevelUpdate();
            Managers.Data.Save();
        }

        //if(!Managers.Data.user.Tutorial)
        //{
        //    if(levelUpCount != 0)
        //    {
        //        yield return new WaitUntil(() => !Managers.UI.Get<UserLevelUp_UI>().gameObject.activeSelf);
        //    }

        //    Managers.UI.Show<Tutorial_MaskImage_UI>();

        //    yield return null;

        //    yield return new WaitUntil(() => !Managers.UI.Get<UserLevelUp_UI>());

        //    Managers.UI.Show<Tutorial_MaskImage_UI>();
        //}
    }
}