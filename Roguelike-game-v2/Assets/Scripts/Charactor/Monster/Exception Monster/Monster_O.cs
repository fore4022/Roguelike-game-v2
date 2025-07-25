using UnityEngine;
/// <summary>
/// 잃은 체력에 비해서 능력치가 상승한다.
/// </summary>
public class Monster_O : BasicMonster
{
    [SerializeField]
    private Color targetColor;
    [SerializeField]
    private float multiplier;

    private float healthLossRatio;
    
    protected override void Init()
    {
        base.Init();

        onDamaged += HealthLossRatioUpdate;
        onDamaged += ColorUpdate;
    }
    private void ColorUpdate()
    {
        render.color = Color.Lerp(defaultColor, targetColor, healthLossRatio);
    }
    private void HealthLossRatioUpdate()
    {
        healthLossRatio = 1 - (health / maxHealth);
        damageMultiplier = speedMultiplier = multiplier * healthLossRatio;
    }
}