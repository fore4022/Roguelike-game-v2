using UnityEngine;
public class GameManager
{
    public Player player;

    public void GameStart()
    {
        //player = GameObject.Instantiate("");
    }
    public void GameEnd()
    {
        Time.timeScale = 0;
    }
}