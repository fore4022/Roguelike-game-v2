using UnityEngine;
public class DifficultyScaler : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.difficultyScaler = this;
    }
    public void Set()
    {

    }
}