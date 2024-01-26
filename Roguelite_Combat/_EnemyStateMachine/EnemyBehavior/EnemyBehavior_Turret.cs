using UnityEngine;

public class EnemyBehavior_Turret : EnemyBehavior
{
    [Header("EnemyBehavior_Turret Settings")]
    [SerializeField] protected ProjectileObjectPool projectilePool;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int poolSize;

    //[SerializeField] protected float startingYRotation;



    public ProjectileObjectPool ProjectilePool => projectilePool;
    public GameObject ProjectilePrefab => projectilePrefab;
    public int PoolSize => poolSize;
    public GameObject GetProjectileFromPool => projectilePool.GetObjectFromPool();

    //public float StartingYRotation => startingYRotation;


    //protected override void OnEnable()
    //{
    //    base.OnEnable();

    //    startingYRotation = transform.rotation.y;
    //}

    protected virtual void Awake()
    {
        if (projectilePrefab.GetComponent<IProjectile>() != null)
        {
            projectilePool.InitializePool(projectilePrefab, poolSize);
        }
        else
        {
            Debug.Log("IProjectile missing on Projectile, cannot Initialize pool");
        }
    }
}
