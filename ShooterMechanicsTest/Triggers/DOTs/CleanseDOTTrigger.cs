using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseDOTTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<HPDamageOverTime>() != null)
            {
                if (other.GetComponent<HPDamageOverTime>().IsTakingDOT())
                {
                    other.GetComponent<HPDamageOverTime>().CancelDOT();
                    Debug.Log("Player triggered CleanseDOT");
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("DamageOverTime script null");
            }
        }
    }
}
