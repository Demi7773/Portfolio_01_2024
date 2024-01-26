using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsInstanced : MonoBehaviour
{
    //[SerializeField] Transform WaypointHolder;
    [SerializeField] Vector3[] _points;
    [SerializeField] int _numberOfWaypoints;

    private void OnValidate()
    {
        _numberOfWaypoints = transform.childCount;
        _points = new Vector3[_numberOfWaypoints];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i).transform.position;
            //var child = transform.GetChild(i);
            //_points[i] = child.transform.worldToLocalMatrix.GetPosition();
        }
    }

    private void OnEnable()
    {
        //_numberOfWaypoints = transform.childCount;
        //_points = new Vector3[_numberOfWaypoints];

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    _points[i] = transform.GetChild(i).transform.position;
        //    //var child = transform.GetChild(i);
        //    //_points[i] = child.transform.worldToLocalMatrix.GetPosition();
        //}
    }

    public Vector3[] point => _points;
    public int numberofWaypoints => _numberOfWaypoints;
}
