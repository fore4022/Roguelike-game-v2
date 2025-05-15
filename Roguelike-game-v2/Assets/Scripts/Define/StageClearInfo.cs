[System.Serializable]
public class StageClearInfo
{
    public StageState state;
    public string name;

    public StageClearInfo(string name, StageState state)
    {
        this.name = name;
        this.state = state;
    }
}