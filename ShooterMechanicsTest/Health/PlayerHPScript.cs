using System.Collections;
using UnityEngine;

public class PlayerHPScript : HPScriptBase
{
    [SerializeField] protected float invulnerabilityDurationOnHit;
    [SerializeField] protected bool isDamageable = true;
    [SerializeField] private PlayerUI playerUIScript;
        
    [SerializeField] protected float baseMaxHP;
    


    // Return methods for reference
    public virtual bool IsDamageable() 
        { return isDamageable; }
    public virtual bool IsHealable() 
    {
        if (hpCurrent < hpMax)
            { return true; } 
        else 
            { return false; }
    }
    public virtual float CurrentHPRatio() 
        { return hpCurrent / hpMax; }



    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        playerUIScript = GetComponentInChildren<PlayerUI>();
        SetMaxHP(baseMaxHP);
        HealToFull();
    }



    // Basic methods overrides - LoseHP, GainHP, Death
    // Add FX
    public override void LoseHP(float dmgAmount)
    {
        if (isDamageable)
        {
            hpCurrent -= dmgAmount;
            hpCurrent = Mathf.Clamp(hpCurrent, 0f, hpMax);
            //Debug.Log("Player Damaged for: " + dmgAmount + "hp");
            playerUIScript.PlayerHPCurrentUI(CurrentHPRatio());
            if (hpCurrent <= 0f)
            {
                Death();
            }

            Invulnerability(invulnerabilityDurationOnHit);
        }
    }
    public virtual void LoseHPDOT(float dmgAmount)
    {
        hpCurrent -= dmgAmount;
        hpCurrent = Mathf.Clamp(hpCurrent, 0f, hpMax);
        //Debug.Log("Player Damaged (no invulnerability) for: " + dmgAmount + "hp");
        playerUIScript.PlayerHPCurrentUI(CurrentHPRatio());
        if (hpCurrent <= 0f)
        {
            Death();
        }
    }
    public override void HealHP(float healAmount)
    {
        if (IsHealable() /*isHealable*/)
        {
            hpCurrent += healAmount;
            hpCurrent = Mathf.Clamp(hpCurrent, 0f, hpMax);
            //Debug.Log("Player Healed for: " + healAmount + "hp");
            playerUIScript.PlayerHPCurrentUI(CurrentHPRatio());
        }
        else
        {
            Debug.Log("Cannot heal, !isHealable");
        }
    }
    // Add Death Mechanics
    protected override void Death()
    {
        this.gameObject.SetActive(false);
    }



    // SetMaxHP
    public virtual void SetMaxHP(float newMaxHP)
    {
        hpMax = newMaxHP;
        playerUIScript.PlayerHPCurrentUI(CurrentHPRatio());
    }



    // Invulnerability mechanic
    public virtual void Invulnerability(float duration)
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
}
