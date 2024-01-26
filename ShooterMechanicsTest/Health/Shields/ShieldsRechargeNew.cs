using System.Collections;
using UnityEngine;

public class ShieldsRechargeNew : MonoBehaviour
{
    protected IShieldable shieldScript;

    [SerializeField] protected float timeBeforeRecharge;
    [SerializeField] protected float timeBetweenRechargeTicks;
    [SerializeField] protected float healShieldsValuePerRechargeTick;

    [SerializeField] private bool isRegening;



    public bool IsRegening() { return isRegening; }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }



    public virtual void InitializeFromShieldsBase
        (IShieldable newShieldScript, float newTimeBeforeRecharge, 
         float newTimeBetweenRechargeTicks, float newHealShieldsValuePerRechargeTick)
    {
        shieldScript = newShieldScript;
        timeBeforeRecharge = newTimeBeforeRecharge;
        timeBetweenRechargeTicks = newTimeBetweenRechargeTicks;
        healShieldsValuePerRechargeTick = newHealShieldsValuePerRechargeTick;
    }


    protected virtual IEnumerator TimerForAutoRechargeStart()
    {
        yield return new WaitForSeconds(timeBeforeRecharge);
        isRegening = true;
        StartCoroutine(AutoRechargeTimer());
        yield return null;
    }
    protected virtual IEnumerator AutoRechargeTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenRechargeTicks);
            if (isRegening)
            {
                shieldScript.HealShield(healShieldsValuePerRechargeTick);
            }
            else
            {
                StopAllCoroutines();
                break;
            }
        }
    }


    public virtual void OnTakeDmg()
    {
        isRegening = false;
        StopAllCoroutines();
        StartCoroutine(TimerForAutoRechargeStart());
    }
}
