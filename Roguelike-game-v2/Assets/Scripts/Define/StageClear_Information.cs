/// <summary>
/// �������� �̸��� �������� ���¸� ������ Ÿ��
/// </summary>
[System.Serializable]
public class StageClear_Information
{
    public StageState state;

    public string name;

    public StageClear_Information(string name, StageState state)
    {
        this.name = name;
        this.state = state;
    }
}