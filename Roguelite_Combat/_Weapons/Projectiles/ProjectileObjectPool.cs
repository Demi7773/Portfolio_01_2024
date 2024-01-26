using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] protected Transform poolParent;
    [SerializeField] protected Queue<GameObject> projectilesQueue = new Queue<GameObject>();
    [SerializeField] protected int checkIfRunningEmptyThreshold = 10;
    [SerializeField] protected int refillAmount = 100;


        // Pool Behavior
    public virtual void AddObjectToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        projectilesQueue.Enqueue(poolObject);
    }
    public GameObject GetObjectFromPool()
    {
        GameObject objectFromPool = projectilesQueue.Dequeue();

        if (projectilesQueue.Count >= checkIfRunningEmptyThreshold)
        {
            //objectFromPool.SetActive(true);
            return objectFromPool;
        }
        else
        {
            Debug.Log("Pool running empty, refilling");
            
            for (int i = 0; i < refillAmount; i++)
            {
                InitializePool(objectFromPool, refillAmount);
            }

            return objectFromPool;
        }
    }



        // Initialization
    public virtual void InitializePool(GameObject objectPrefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            SetUpInstance(objectPrefab);
        }
    }        
        // Initialization Behavior per instance
    protected virtual void SetUpInstance(GameObject objectPrefab)
    {
        GameObject objectInstance = Instantiate(objectPrefab, poolParent);

        IProjectile projectileScript = objectInstance.GetComponent<IProjectile>();
        projectileScript.SetObjectPoolReference(this);

        AddObjectToPool(objectInstance);
    }

    
}
