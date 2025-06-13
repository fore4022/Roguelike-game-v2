using System.Collections;
using UnityEngine;
public class MonsterSkill_A : MonsterSkillBase
{
    [SerializeField]
    private float speed;

    private Vector3 direction;

    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void SetActive(bool isActive)
    {
        base.SetActive(isActive);

        col.enabled = isActive;
    }
    private IEnumerator Casting()
    {
        direction = Calculate.GetDirection(Managers.Game.player.transform.position, transform.position);
        transform.rotation = Calculate.GetQuaternion(direction);

        SetActive(true);

        while(true)
        {
            transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
    }
}