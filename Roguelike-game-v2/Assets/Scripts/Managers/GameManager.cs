using UnityEngine;
public class GameManager
{
    public Player player;

    public void GameStart()
    {

    }
    public void GameEnd()
    {
        Time.timeScale = 0;
    }
}