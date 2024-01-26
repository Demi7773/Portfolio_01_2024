using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private int numberOfWaypoints;



    private void OnValidate()
    {
        numberOfWaypoints = transform.childCount;

        for (int i = 0; i < numberOfWaypoints; i++)
        {
            points.Add(transform.GetChild(i));
        }
    }
    


    public List<Transform> Points => points;
    public int NumberOfWaypoints => numberOfWaypoints;
}
