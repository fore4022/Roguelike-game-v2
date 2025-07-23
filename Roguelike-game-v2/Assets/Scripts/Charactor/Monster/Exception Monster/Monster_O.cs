using UnityEngine;
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