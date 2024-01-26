using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgShieldsToEmptyTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerPlayer");
            if (other.GetComponent<PlayerShields>() != null)
            {
                PlayerShields playerShields = other.GetComponent<PlayerShields>();

                if (playerShields.IsDamageable() && playerShields.ShieldsCurrentValue() > 0f)
                {
                    playerShields.InstaLoseAllShields();
                    Debug.Log("Player triggered DmgShieldsToEmpty");
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
