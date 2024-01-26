using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolNew : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] private Queue<GameObject> projectilePool = new();
    [SerializeField] private float poolPosX, poolPosY, poolPosZ;

    [Header("Set from Scriptable")]
    [SerializeField] private int poolAmount;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int totalSpawnedCount = 0;



    private void Awake()
    {
        projectilePool.Clear();
        totalSpawnedCount = 0;
        InitializeBulletPool();
    }

    private void InitializeBulletPool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            var projectile = Instantiate(projectilePrefab);
            // var projectileScript = proj.GetComponent<***OVDJE DODAT CANNONBALL SKRIPTU***>();
            // projectileScript.SetObjectPoolReference(this);

            // ***ovdje setat statse od Cannonballa ako hoces***

            AddObjectToPool(projectile);
        }
    }



    public void AddObjectToPool(GameObject projectile)
    {
        projectile.SetActive(false);
        ResetProjectilePosition(projectile);
        projectilePool.Enqueue(projectile);
    }
    public GameObject GetObjectFromPool()
    {
        GameObject projectile = projectilePool.Dequeue();
        return projectile;
    }



    public void ResetProjectilePosition(GameObject projectile)
    {
        projectile.transform.position = new Vector3(poolPosX + totalSpawnedCount, poolPosY, poolPosZ);
        totalSpawnedCount++;
    }
}
