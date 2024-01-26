using UnityEngine;

public class DmgTrigger : MonoBehaviour
{
    [SerializeField] protected float dmgAmount;

    protected virtual void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerPlayer");
            if (other.GetComponent<PlayerShields>() != null)
            {
                PlayerShields playerShields = other.GetComponent<PlayerShields>();

                if (playerShields.IsDamageable())
                {
                    playerShields.LoseShield(dmgAmount);
                    //Debug.Log("Player triggered Dmg Trigger");
                    gameObject.SetActive(false);
                }
            }

            else if (other.GetComponent<PlayerHPScript>() == null)
            {
                Debug.Log("Triggered with null PlayerShields and PlayerHPScript");
            }

            else if (other.GetComponent<PlayerHPScript>() != null)
            {
                PlayerHPScript playerHPScript = other.GetComponent<PlayerHPScript>();

                if (playerHPScript.IsDamageable())
                {
                    playerHPScript.LoseHP(dmgAmount);
                    //Debug.Log("Player triggered Dmg Trigger");
                    gameObject.SetActive(false);
                }
            }

            else
            {
                Debug.Log("??? idk");
            }
        }
    }
}
