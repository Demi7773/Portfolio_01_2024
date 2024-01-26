using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectiles : PassiveEffect
{
    [SerializeField] protected List<Transform> shootPositions = new List<Transform>();
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileLifetime;
    [SerializeField] protected float projectileForwardSpeed;

    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform projectilesParent;
    [SerializeField] protected int poolSize = 50;
    [SerializeField] protected Queue<GameObject> projectilesQueue = new Queue<GameObject>();



    protected virtual void OnEnable()
    {
        InitializePool();
    }

    public virtual void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectilesParent);

            if (projectile.GetComponent<Projectile>() != null)
            {
                projectile.GetComponent<Projectile>().InitializeMe(this, projectileDamage, projectileLifetime, projectileForwardSpeed);
            }
            else
            {
                Debug.LogError("Projectile Script null!");
            }

            projectile.SetActive(false);
        }
    }
    public virtual void ReturnToQueue(GameObject projectile)
    {
        projectilesQueue.Enqueue(projectile);
    }



        // Maybe needs timer reset after leaving and returning to pool if Update is used
    protected override void ActivateEffectProc()
    {
        foreach (Transform shootPos in shootPositions)
        {
            GameObject projectile = projectilesQueue.Dequeue();

            projectile.transform.position = shootPos.position;
            projectile.transform.rotation = shootPos.rotation;

            projectile.SetActive(true);
        }
    }
}
