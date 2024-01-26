using UnityEngine;

public abstract class HPScriptBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float hpCurrent;
    [SerializeField] protected float hpMax;


    // Basic methods - LoseHP, GainHP, Death
    // Override for unique mechanics (like invulnerability)
    // Add death mechanic specifics and UI system
    public virtual void LoseHP(float dmgAmount)
    {
        float newHP = Mathf.Clamp(hpCurrent - dmgAmount, 0f, hpMax);
        hpCurrent = newHP;
        if (hpCurrent <= 0f)
        {
            Death();
        }
    }
    public virtual void HealHP(float healAmount)
    {
        float newHP = Mathf.Clamp(hpCurrent + healAmount, 0f, hpMax);
        hpCurrent = newHP;
    }
    protected virtual void Death()
    {
        this.gameObject.SetActive(false);
    }



    // All methods below send values to LoseHP() and HealHP()

    // Conversions for percent Dmg and Heal
    public virtual void LoseHPPercentCurrent(float dmgPercent)
    {
        float dmgAmount = ExtensionMethods.CalculateNumberFromPercentage(dmgPercent, hpCurrent);
        LoseHP(dmgAmount);
        //Debug.Log("LoseHPPercentCurrent " + dmgPercent + "% for " + dmgAmount);
    }
    public virtual void LoseHPPercentMax(float dmgPercent)
    {
        float dmgAmount = ExtensionMethods.CalculateNumberFromPercentage(dmgPercent, hpMax);
        LoseHP(dmgAmount);
        //Debug.Log("LoseHPPercentMax " + dmgPercent + "% for " + dmgAmount);
    }
    public virtual void HealHPPercentCurrent(float healPercent)
    {
        float healAmount = ExtensionMethods.CalculateNumberFromPercentage(healPercent, hpCurrent);
        HealHP(healAmount);
        //Debug.Log("HealHPPercentCurrent " + healPercent + "% for " + healAmount);
    }
    public virtual void HealHPPercentMax(float healPercent)
    {
        float healAmount = ExtensionMethods.CalculateNumberFromPercentage(healPercent, hpMax);
        HealHP(healAmount);
        //Debug.Log("HealHPPercentMax " + healPercent + "% for " + healAmount);
    }

    // Methods for InstaKill and HealToFull
    public virtual void InstaKill()
    {
        LoseHP(Mathf.Infinity);
        Debug.Log("Instakill");
    }
    public virtual void HealToFull()
    {
        HealHP(Mathf.Infinity);
        //Debug.Log("Healed to full");
    }
}
