using UnityEngine;

public class RegenTrigger : MonoBehaviour
{
    [SerializeField] private float healPerTick;
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private float effectDuration;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHPScript>().IsHealable())
            {
                if (other.GetComponent<HPRegen>() != null)
                {
                    other.GetComponent<HPRegen>().EnableRegen(healPerTick, timeBetweenTicks, effectDuration);
                    //Debug.Log("Player triggered Regen");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("HPRegen script null");
                }
            }
        }
    }
}
