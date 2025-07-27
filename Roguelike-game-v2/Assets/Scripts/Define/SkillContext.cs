public class SkillContext
{
    public SkillInformation data;
    public SkillCaster caster = null;

    public int level = 0;

    public SkillContext(SkillInformation_SO dataSO)
    {
        data = new(dataSO);
    }
}