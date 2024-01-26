using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] protected LayerMask explosionLayers;

    [SerializeField] protected float radius;
    [SerializeField] protected float vfxDuration;
    [SerializeField] protected float damage;

    //[SerializeField] protected float explosionDuration;
    [SerializeField] protected GameObject explosionVFX;



    public void Explode(Transform explosionSource /*Vector3 explosionCenter*/)
    {
        Debug.Log("Self Destruct started");

        PlayExplosionVFX();

        Collider[] collidersHit = Physics.OverlapSphere(explosionSource.position, radius, explosionLayers);
        foreach (Collider collider in collidersHit)
        {
            Debug.Log("Self Destruct hit target");
            EffectOnHitTarget(collider);
        }

        explosionSource.gameObject.SetActive(false);
    }

    private void PlayExplosionVFX()
    {
        GameObject explosionSpawn = Instantiate(explosionVFX);
        if (explosionSpawn.GetComponent<ExplosionVFXHolder>() != null)
        {
            explosionSpawn.GetComponent<ExplosionVFXHolder>().PlayAnimation(vfxDuration, radius);
        }
        else
        {
            Debug.Log("ExplosionVFXHolder null on explosion");
        }
    }

    protected virtual void EffectOnHitTarget(Collider hitTarget)
    {
        IDamageable damageable = hitTarget.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.GetHitFor(damage);
        }
        else
        {
            Debug.Log("IDamageable null on hitTarget!");
        }
    }
}
