using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] protected List<Transform> spawnPoints = new List<Transform>();



    protected void OnEnable()
    {
        AddChildrenToSpawnPointsList();
    }
    protected void AddChildrenToSpawnPointsList()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            spawnPoints.Add(child);
        }
    }



    public Transform GetRandomSpawnPointFromList()
    {
        int roll = Random.Range(0, spawnPoints.Count);
        return spawnPoints[roll];
    }
}
