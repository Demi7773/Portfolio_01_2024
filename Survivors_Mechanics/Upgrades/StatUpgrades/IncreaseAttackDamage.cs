using UnityEngine;

public class IncreaseAttackDamage : Upgrade
{
    [SerializeField] protected float increaseDamageBy = 0.5f;



    public override void ApplyThisUpgradeToPlayer()
    {
        playerStats.ChangeAttackDamageMultiplier(increaseDamageBy);
        Debug.Log("DamageMultiplier increased by: " + increaseDamageBy);
    }
}
