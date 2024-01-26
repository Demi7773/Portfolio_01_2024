using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] private Queue<GameObject> projectilePool = new();
    [SerializeField] private float poolPosX, poolPosY, poolPosZ;
    [SerializeField] private GunScriptable gunScriptable;

    [Header("Set from Scriptable")]
    [SerializeField] private int poolAmount;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int totalSpawnedCount = 0;



    private void Awake()
    {
        projectilePool.Clear();
        totalSpawnedCount = 0;
        GetStatsFromScriptable();
        InitializeBulletPool();
    }



    private void GetStatsFromScriptable()
    {
        poolAmount = gunScriptable.MaxAmmo;
        projectilePrefab = gunScriptable.ProjectilePrefab;
    }



    private void InitializeBulletPool()
    {
        float projectileSpd = gunScriptable.ProjectileSpeed;
        float projectileDmg = gunScriptable.ProjectileDmg;

        for (int i = 0; i < poolAmount; i++)
        {
            var proj = Instantiate(projectilePrefab);
            var projScript = proj.GetComponent<ProjectileScriptBase>();
            projScript.SetAmmoScript(this);
            projScript.SetSpeedAndDmg(projectileSpd, projectileDmg);
            AddObjectToPool(proj);
        }
    }



    public void AddObjectToPool(GameObject projectile)
    {
        projectile.SetActive(false);
        ResetProjectilePosition(projectile);
        projectilePool.Enqueue(projectile);
    }
    public GameObject RemoveObjectFromPool()
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
