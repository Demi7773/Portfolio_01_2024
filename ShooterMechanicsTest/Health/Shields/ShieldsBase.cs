using UnityEngine;

public abstract class ShieldsBase : MonoBehaviour, IShieldable
{
    protected IDamageable hpScript;
    protected ShieldsRechargeNew shieldsRechargeScript;

    [Header("Basic Values")]
    [SerializeField] protected float shieldsCurrentValue;
    [SerializeField] protected float shieldsMaxValue;

    [Header("Recharge Values")]
    [SerializeField] protected float timeBeforeRecharge;
    [SerializeField] protected float timeBetweenRechargeTicks;
    [SerializeField] protected float healShieldsValuePerRechargeTick;
    [SerializeField] protected float healPerSecForUI;



    public virtual bool IsShielded()
    {
        if (shieldsCurrentValue > 0f)
            return true;
        else
            return false;
    }
    public virtual float ShieldsCurrentValue() { return shieldsCurrentValue; }



    protected virtual void Awake()
    {
        hpScript = GetComponent<IDamageable>();
        shieldsRechargeScript = GetComponent<ShieldsRechargeNew>();
    }
    protected virtual void Start()
    {
        shieldsRechargeScript.InitializeFromShieldsBase(this, timeBeforeRecharge, timeBetweenRechargeTicks, healShieldsValuePerRechargeTick);
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }



    public virtual void SetValuesFromShieldsHolder
        (float newMaxShieldsValue, float newTimeBeforeRecharge,
         float newTimeBetweenRechargeTicks, float newHealShieldsValuePerRechargeTick, float newHealPerSecForUI)
    {
        shieldsMaxValue = newMaxShieldsValue;
        timeBeforeRecharge = newTimeBeforeRecharge;
        timeBetweenRechargeTicks = newTimeBetweenRechargeTicks;
        healShieldsValuePerRechargeTick = newHealShieldsValuePerRechargeTick;
        healPerSecForUI = newHealPerSecForUI;


        HealShieldsToFull();
    }



    public virtual void LoseShield(float dmgAmount)
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
            hpScript.LoseHP(resultAfterDmg);
            Debug.Log("Shields broken, taking " + resultAfterDmg + "hp damage");
        }
    }
    public virtual void HealShield(float healAmount)
    {
        float newShieldValue = Mathf.Clamp(shieldsCurrentValue + healAmount, 0f, shieldsMaxValue);
        shieldsCurrentValue = newShieldValue;
    }



    public virtual void LoseShieldPercentCurrent(float dmgPercent)
    {
        float dmgAmount = ExtensionMethods.CalculateNumberFromPercentage(dmgPercent, shieldsCurrentValue);
        LoseShield(dmgAmount);
    }
    public virtual void LoseShieldPercentMax(float dmgPercent)
    {
        float dmgAmount = ExtensionMethods.CalculateNumberFromPercentage(dmgPercent, shieldsMaxValue);
        LoseShield(dmgAmount);
    }
    public virtual void HealShieldPercentCurrent(float healPercent)
    {
        float healAmount = ExtensionMethods.CalculateNumberFromPercentage(healPercent, shieldsCurrentValue);
        HealShield(healAmount);
    }
    public virtual void HealShieldPercentMax(float healPercent)
    {
        float healAmount = ExtensionMethods.CalculateNumberFromPercentage(healPercent, shieldsMaxValue);
        HealShield(healAmount);
    }



    public virtual void HealShieldsToFull()
    {
        HealShield(Mathf.Infinity);
        Debug.Log("Healed Shields to full");
    }
    public virtual void InstaLoseAllShields()
    {
        shieldsCurrentValue = 0f;
        shieldsRechargeScript.OnTakeDmg();
    }
}
