using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// InGame Camera ����
/// </para>
/// �÷��̾ ���� �̵�, GameOver ī�޶� ����
/// </summary>
public class InGame_Camera : MonoBehaviour
{
    private GameObject player = null;

    private const float zpos = -10;

    private void Update()
    {
        if(player == null)
        {
            if(Managers.Game.player != null)
            {
                player = Managers.Game.player.gameObject;
            }
            else
            {
                return;
            }
        }

        if(!Managers.Game.player.Death)
        {
            if(Managers.Game.Playing)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zpos);
            }
        }
    }
}