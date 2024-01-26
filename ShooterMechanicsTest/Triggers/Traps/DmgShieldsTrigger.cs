using UnityEngine;

public class DmgShieldsTrigger : DmgTrigger
{
    protected override void OnTriggerStay(Collider other)
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
                    float currentShieldsValue = playerShields.ShieldsCurrentValue();
                    float finalDmgAmount;

                    if (dmgAmount <= currentShieldsValue)
                    {
                        finalDmgAmount = dmgAmount;
                    }
                    else
                    {
                        finalDmgAmount = currentShieldsValue;
                    }

                    playerShields.LoseShield(finalDmgAmount);
                    //Debug.Log("Player triggered DmgShields Trigger for " +  + "dmg");
                    gameObject.SetActive(false);
                }
            } 
        }
    }
}
