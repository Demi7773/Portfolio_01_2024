using UnityEngine;

public class BoostDefense : Upgrade
{
    [SerializeField] protected float maxHPMultiplier = 0.2f;
    [SerializeField] protected float armorGain = 10.0f;



    public override void ApplyThisUpgradeToPlayer()
    {
        playerStats.ChangeMaxHPMultiplier(maxHPMultiplier);
        playerStats.ChangeArmor(armorGain);
        Debug.Log("BoostDefense Upgrade applied");
    }
}
