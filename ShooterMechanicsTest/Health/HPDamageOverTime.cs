using System.Collections;
using UnityEngine;

public class HPDamageOverTime : MonoBehaviour
{
    // player only for now, add new method to enemy script later
    protected PlayerHPScript hpScript;
    protected bool isTakingDOT = false;



    public virtual bool IsTakingDOT() {  return isTakingDOT; }



    protected virtual void Awake()
    {
        hpScript = GetComponent<PlayerHPScript>();
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void EnableDOT(float dmgAmount, float timeBetweenTicks, float effectDuration)
    {
        //Debug.Log("Enable DOT");
        StartCoroutine(RegenerationCoroutine(dmgAmount, timeBetweenTicks, effectDuration));
    }
    public virtual void CancelDOT()
    {
        Debug.Log("Cancel DOT");
        StopAllCoroutines();
        isTakingDOT = false;
    }
    protected IEnumerator RegenerationCoroutine(float dmgAmount, float timeBetweenTicks, float effectDuration)
    {
        isTakingDOT = true;
        int targetNumberOfRepeats = (int)(effectDuration / timeBetweenTicks);
        int currentNumberOfRepeats = 0;

        while (hpScript != null)
        {
            hpScript.LoseHPDOT(dmgAmount);
            currentNumberOfRepeats++;
            //Debug.Log("DOT tick number " + currentNumberOfRepeats + " for " + dmgAmount + "hp");
            if (currentNumberOfRepeats < targetNumberOfRepeats)
            {
                yield return new WaitForSeconds(timeBetweenTicks);
            }
            else
            {
                //Debug.Log("DOT reached target, break");
                isTakingDOT = false;
                break;
            }
        }
    }
}
