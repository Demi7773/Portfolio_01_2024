using UnityEngine;

public class IncreaseSpecialAttackDamage : Upgrade
{
    [SerializeField] protected float increaseMultiplierBy = 0.5f;



    public override void ApplyThisUpgradeToPlayer()
    {
        playerStats.ChangeSpecialAttackDamageMultiplier(increaseMultiplierBy);
        Debug.Log("IncreaseSpecialAttackDamage Upgrade applied");
    }
}
