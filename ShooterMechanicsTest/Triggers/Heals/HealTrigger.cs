using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    [SerializeField] private float healAmount;



    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerPlayer");
            if (other.GetComponent<PlayerHPScript>().IsHealable())
            {
                other.GetComponent<PlayerHPScript>().HealHP(healAmount);
                //Debug.Log("Player triggered Heal");
                gameObject.SetActive(false);
            }
        }
    }
}
