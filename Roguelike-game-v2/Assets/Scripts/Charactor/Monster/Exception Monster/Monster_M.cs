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

    private float value;

    protected override void Enable()
    {
        value = Random.Range(scaleValue_Min, scaleValue_Max + 1);
        transform.localScale = new(value, value);
        defaultColor = colors[Random.Range(0, colors.Count)];
     
        base.Enable();
    }
    protected override void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

        value /= 3;
        go.transform.position = transform.position;
        go.transform.localScale = new(value, value);
        go.render.color = render.color;

        go.SetActive(true);
    }
}