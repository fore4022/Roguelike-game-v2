/// <summary>
/// �÷��̾� ��ų�� ������ Caster, ������ �����ϴ� Ÿ��
/// </summary>
public class SkillContext
{
    public Skill_Information data;
    public SkillCaster caster = null;

    public int level = 0;

    public SkillContext(SkillInformation_SO dataSO)
    {
        data = new(dataSO);
    }
}