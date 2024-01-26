using System.Collections;
using UnityEngine;

public class PlayerShields : ShieldsBase
{
    private PlayerUI playerUIScript;
    
    [SerializeField] private bool isDamageable = true;

    [SerializeField] protected float invulnerabilityDurationOnHit;


    public virtual float PlayerCurrentShieldRatio()
    {
        return (shieldsCurrentValue / shieldsMaxValue);
    }
    public virtual bool IsDamageable() { return isDamageable; }



    protected override void Awake()
    {
        playerUIScript = GetComponentInChildren<PlayerUI>();
        base.Awake();
    }



    // Invulnerability mechanic
    protected void Invulnerability(float duration)
    {
        StartCoroutine("InvulnerabilityTimer", duration);
    }
    protected IEnumerator InvulnerabilityTimer(float duration)
    {
        isDamageable = false;
        Debug.Log("!isDamageable");
        yield return new WaitForSeconds(duration);
        isDamageable = true;
        Debug.Log("isDamageable");
    }



    public override void SetValuesFromShieldsHolder
        (float newMaxShieldsValue, float newTimeBeforeRecharge, 
         float newTimeBetweenRechargeTicks, float newHealShieldsValuePerRechargeTick, float newHealPerSecForUI)
    {
        base.SetValuesFromShieldsHolder(newMaxShieldsValue, newTimeBeforeRecharge, newTimeBetweenRechargeTicks, 
                                        newHealShieldsValuePerRechargeTick, newHealPerSecForUI);

        playerUIScript.PlayerShieldsCurrentUI(PlayerCurrentShieldRatio());
    }



    public override void LoseShield(float dmgAmount)
    {
        if (isDamageable)
        {
            shieldsRechargeScript.OnTakeDmg();

            float resultAfterDmg = shieldsCurrentValue - dmgAmount;
            if (resultAfterDmg >= 0f)
            {
                shieldsCurrentValue = resultAfterDmg;
            }
            else
            {
                shieldsCurrentValue = 0f;
                hpScript.LoseHP(-resultAfterDmg);
                Debug.Log("PlayerShields broken, taking " + -resultAfterDmg + "hp damage");
            }

            playerUIScript.PlayerShieldsCurrentUI(PlayerCurrentShieldRatio());

            StartCoroutine(InvulnerabilityTimer(invulnerabilityDurationOnHit));
        }
    }
    public override void HealShield(float healAmount)
    {
        base.HealShield(healAmount);
        playerUIScript.PlayerShieldsCurrentUI(PlayerCurrentShieldRatio());
    }


    public override void InstaLoseAllShields()
    {
        base.InstaLoseAllShields();
        playerUIScript.PlayerShieldsCurrentUI(PlayerCurrentShieldRatio());
    }
}
