using UnityEngine;

public class HPDOTTrigger : MonoBehaviour
{
    [SerializeField] float dmgPerTick;
    [SerializeField] float timeBetweenTicks;
    [SerializeField] float effectDuration;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<HPDamageOverTime>() != null)
            {
                if (other.GetComponent<PlayerHPScript>() != null)
                {
                    if (other.GetComponent<PlayerHPScript>().IsDamageable())
                    {
                        other.GetComponent<HPDamageOverTime>().EnableDOT(dmgPerTick, timeBetweenTicks, effectDuration);
                        Debug.Log("DOT started on player");
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Player !IsDamageable");
                    }
                }
            }
            else
            {
                Debug.Log("DamageOverTime script null");
            }
        }
    }
}
