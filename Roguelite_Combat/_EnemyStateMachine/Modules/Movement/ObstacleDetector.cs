using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] private int priority = 3;
    [SerializeField] private float rayMaxDistance = 10.0f;
    //[SerializeField] private float objectRangeToLowerPrio1 = 6.0f;
    //[SerializeField] private float objectRangeToLowerPrio2 = 3.0f;
    //[SerializeField] private float objectRangeToLowerPrio3 = 1.0f;
    [SerializeField] private LayerMask hitLayers;

    public int Priority => priority;

    //public enum Direction
    //{
    //    Fwd,
    //    FwdR,
    //    R,
    //    BwdR,
    //    Bwd,
    //    BwdL,
    //    L,
    //    FwdL,
    //}


    public void SetNewPrio(int newPrio)
    {
        priority = newPrio;
        Debug.DrawLine(transform.position, transform.position + transform.forward * priority, Color.red);
    }

    public float ObstacleScanDistance()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, rayMaxDistance, hitLayers))
        {
            if (hitInfo.collider != null)
            {
                //Debug.Log("Raycast hit");
                return hitInfo.distance;
            }
        }

        return 100.0f;
    }

    public void AdjustPrioBy(int adjustment)
    {
        priority += adjustment;
    }

    public void DrawMyPrioLength()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * priority, Color.red, 1.0f);
    }

    //public void ScanForObstaclesAndAdjustPrio()
    //{
    //    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, rayMaxDistance, hitLayers))
    //    {
    //        if (hitInfo.collider != null)
    //        {
    //            //Debug.Log("Raycast hit");
    //            if (hitInfo.distance < objectRangeToLowerPrio1)
    //            {
    //                //Debug.Log("Prio -1");
    //                priority--;

    //                if (hitInfo.distance < objectRangeToLowerPrio2)
    //                {
    //                    //Debug.Log("Prio -2");
    //                    priority--;
    //                }
    //            }
    //            else
    //            {
    //                //Debug.Log("Hit Collider but distance too far to affect prio");
    //                priority = 3;
    //            }
    //        }
    //        else
    //        {
    //            //Debug.Log("Hit Collider null");
    //            priority = 3;
    //        }
    //    }
    //    else
    //    {
    //        priority = 3;
    //        //Debug.Log("Raycast false");
    //    }

    //    Debug.DrawLine(transform.position, transform.position + transform.forward * priority, Color.red);
    //}
}
