using UnityEngine;

public class Projectile_Base : MonoBehaviour, IProjectile
{
    [SerializeField] protected ProjectileObjectPool objectPool;
    [SerializeField] protected float damage;
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float timer;
    [SerializeField] protected float deactivationTime = 3.0f;


    protected virtual void OnEnable()
    {
        timer = 0.0f;
    }
    protected virtual void OnDisable()
    {
        ReturnMeToPool();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= deactivationTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }      
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().GetHitFor(damage);
        }
        else
        {
            Debug.Log("No IDamageable on hit");
        }

        gameObject.SetActive(false);
    }





    public virtual void SetObjectPoolReference(ProjectileObjectPool pool)
    {
        objectPool = pool;
    }
    public virtual void ReturnMeToPool()
    {
        objectPool.AddObjectToPool(gameObject);
    }
    public virtual void SetMyStats(float dmg, float speed)
    {
        damage = dmg;
        moveSpeed = speed;
    }
}
