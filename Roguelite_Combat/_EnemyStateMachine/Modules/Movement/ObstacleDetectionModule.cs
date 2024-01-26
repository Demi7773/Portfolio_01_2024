using UnityEngine;

public class ObstacleDetectionModule : MonoBehaviour
{
    [SerializeField] private int priority = 0;
    [SerializeField] private float minDistanceCutoff = 2.0f;
    [SerializeField] private float maxDistanceCutoff = 5.0f;

    public int Priority => priority;



    //public void SetNewPrio(int newPrio)
    //{
    //    priority = newPrio;
    //    Debug.DrawLine(transform.position, transform.position + transform.forward * priority, Color.red);
    //}



    public void SetPrioDirectionsByObstacles(float scanDistance, LayerMask scanLayers)
    {
        float distance = ObstacleScanDistance(scanDistance, scanLayers);
        int newPrio = 0;

        if (distance < minDistanceCutoff)
        {
            Debug.Log("Obstacle too close, prio -5");
            newPrio = -5;
        }
        else if (distance > maxDistanceCutoff)
        {
            Debug.Log("Obstacle too far, prio set to max");
            newPrio = (int)maxDistanceCutoff;
        }
        else
        {
            newPrio = (int)distance;
            Debug.Log("Obstacle in calculating distance, prio: " +  newPrio);
        }

        priority = newPrio;
    }
    public float ObstacleScanDistance(float rayMaxDistance, LayerMask scanLayers)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, rayMaxDistance, scanLayers))
        {
            if (hitInfo.collider != null)
            {
                //Debug.Log("Raycast hit distance: " + hitInfo.distance);
                return hitInfo.distance;
            }
        }

        return rayMaxDistance;
    }



    public void AdjustPrioBy(int adjustment)
    {
        priority += adjustment;
    }

    public void DrawMyPrioLength()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * priority, Color.red, 1.0f);
    }





    public void DebugLine(float duration)
    {
        Debug.DrawRay(transform.position, transform.forward * priority);
        //Debug.DrawLine(transform.position, transform.forward * priority, Color.red, duration);
    }
}
