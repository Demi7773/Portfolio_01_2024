using System.Collections.Generic;
using UnityEngine;

public class PotHP : ObjectHP
{
    [SerializeField] protected float chanceToDrop = 1.0f;
    [SerializeField] protected List<GameObject> potentialDrops = new List<GameObject>();


        // add vfx and sfx
    protected override void DestroyObject()
    {
        RollForDrops();
        base.DestroyObject();
    }

    protected virtual void RollForDrops()
    {
        float roll = Random.Range(0.0f, 99.0f);
        if (roll <= chanceToDrop)
        {
            int rollReward = Random.Range(0, potentialDrops.Count);
            Instantiate(potentialDrops[rollReward], transform.position, Quaternion.identity);
        }
    }
}
