using System.Collections.Generic;
[System.Serializable]
public class TechnologyList
{
    public enum Type
    {
        Level,
        Currency
    }

    public List<TechnologyNode_SO> nodes = new();

    public Type pointType;
}