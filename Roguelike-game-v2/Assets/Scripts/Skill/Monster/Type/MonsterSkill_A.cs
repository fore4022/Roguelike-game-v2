using System.Collections;
public class MonsterSkill_A : MonsterSkillBase
{
    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void SetActive(bool isActive)
    {
        base.SetActive(isActive);

        //
    }
    protected override void Init()
    {
        base.Init();

        //
    }
    private IEnumerator Casting()
    {
        while(true)
        {
            //

            yield return null;//
        }
    }
}