using System.Collections.Generic;
using UnityEngine;
public class DamageText_Manage : MonoBehaviour
{
    public List<DamageText> damageTexts;
    public GameObject damageText;

    private void Awake()
    {
        Managers.Game.damageText_Creator = this;
    }
    public void Show()
    {

    }
}