using System.Collections;
using UnityEngine;

public class HPRegen : MonoBehaviour
{
    protected IDamageable hpScript;



    protected virtual void Awake()
    {
        hpScript = GetComponent<IDamageable>();
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void EnableRegen(float healAmount, float timeBetweenTicks, float effectDuration)
    {
        //Debug.Log("Enable Regen");
        StartCoroutine(RegenerationCoroutine(healAmount, timeBetweenTicks, effectDuration));
    }
    public virtual void CancelRegen()
    {
        Debug.Log("Cancel Regen");
        StopAllCoroutines();
    }
    protected IEnumerator RegenerationCoroutine(float healAmount, float timeBetweenTicks, float effectDuration)
    {
        int targetNumberOfRepeats = (int)(effectDuration / timeBetweenTicks);
        int currentNumberOfRepeats = 0;

        while (hpScript != null)
        {
            hpScript.HealHP(healAmount);
            currentNumberOfRepeats++;
            //Debug.Log("Regen tick number " + currentNumberOfRepeats + " for " + healAmount + "hp");
            if (currentNumberOfRepeats < targetNumberOfRepeats)
            {
                yield return new WaitForSeconds(timeBetweenTicks);
            }
            else
            {
                Debug.Log("Regen reached target, break");
                break;
            }
        }
    }
}
