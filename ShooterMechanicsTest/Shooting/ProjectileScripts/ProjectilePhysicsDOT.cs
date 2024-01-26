using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilePhysicsDOT : ProjectilePhysics
{
    [SerializeField] private float dmgPerTick;
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private float effectDuration;
    [SerializeField] private HPDamageOverTime dotScript;



    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IShieldable>() != null)
        {
            var shieldScript = collision.gameObject.GetComponent<IShieldable>();
            shieldScript.LoseShield(projectileDmg);
            Debug.Log("Projectile hit IShieldable");
        }
        else if (collision.gameObject.GetComponent<IDamageable>() == null)
        {
            Debug.Log("Projectile hit sth else");
        }
        else
        {
            var hpScript = collision.gameObject.GetComponent<IDamageable>();
            hpScript.LoseHP(projectileDmg);
            Debug.Log("Projectile hit IDamageable");
        }



        if (collision.gameObject.GetComponent<HPDamageOverTime>() != null)
        {
            collision.gameObject.GetComponent<HPDamageOverTime>().EnableDOT(dmgPerTick, timeBetweenTicks, effectDuration);
        }
        else
        {
            Debug.Log("DamageOverTime null");
        }

        this.gameObject.SetActive(false);
    }
}
