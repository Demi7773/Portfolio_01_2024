using System.Collections;
using UnityEngine;

public abstract class ProjectileScriptBase : MonoBehaviour
{
    protected AmmoPool ammoPool;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileDmg;
    [SerializeField] protected float despawnDuration = 3f;



    protected virtual void Awake()
    {

    }
    
    IEnumerator DespawnProjectileAfterTime()
    {
        yield return new WaitForSeconds(despawnDuration);
        Debug.Log("Projectile despawned after " + despawnDuration);
        gameObject.SetActive(false);
    }


    protected virtual void OnCollisionEnter(Collision collision)
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

        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(DespawnProjectileAfterTime());
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
        ammoPool.AddObjectToPool(this.gameObject);
    }


    public void SetAmmoScript(AmmoPool ammoScript)
    {
        ammoPool = ammoScript;
    }
    public void SetSpeedAndDmg(float speed, float dmg)
    {
        projectileSpeed = speed;
        projectileDmg = dmg;
    }
}
