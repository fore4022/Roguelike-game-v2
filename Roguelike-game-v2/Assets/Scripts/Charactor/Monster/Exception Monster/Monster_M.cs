using System.Collections.Generic;
using UnityEngine;
public class Monster_M : Monster_G
{
    [SerializeField]
    private List<Color> colors;
    [SerializeField]
    private float scaleValue_Min;
    [SerializeField]
    private float scaleValue_Max;

    protected override void Enable()
    {
        render.color = colors[Random.Range(0, colors.Count)];

        base.Enable();
    }
    protected override void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

        float value = Random.Range(scaleValue_Min, scaleValue_Max + 1);

        go.transform.position = transform.position;
        go.transform.localScale = new(value, value);

        go.SetActive(true);
    }
}