using UnityEngine;

public class HealShieldTrigger : MonoBehaviour
{
    [SerializeField] protected float healShieldAmount;



    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerPlayer");
            if (other.GetComponent<PlayerShields>().PlayerCurrentShieldRatio() < 1f)
            {
                other.GetComponent<PlayerShields>().HealShield(healShieldAmount);
                //Debug.Log("Player triggered Heal");
                gameObject.SetActive(false);
            }
        }
    }
}
