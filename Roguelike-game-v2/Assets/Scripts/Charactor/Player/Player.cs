using UnityEngine;
public class Player : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerMove = new();
        playerAttack = new();
    }
}
