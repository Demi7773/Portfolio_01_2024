using UnityEngine;

public class Regeneration : PassiveEffect
{
    [SerializeField] private PlayerHP playerHPScript;
    [SerializeField] private float healPerTick;



    protected override void ActivateEffectProc()
    {
        //base.ActivateEffectProc();
        if(playerHPScript.CheckIfHealable_HealIfTrue(healPerTick))
        {
            Debug.Log("Regen tick, Player healed for: " +  healPerTick);
        }
        else
        {
            Debug.Log("Regen tick, playerHP full");
        }
    }
}
