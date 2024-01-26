using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementModule : MonoBehaviour
{
    //[SerializeField] private List<ObstacleDetectionModule> highestPrioDirections = new List<ObstacleDetectionModule>();
    [Header("Choices")]
    [SerializeField] protected List<ObstacleDetectionModule> potentialChoices = new List<ObstacleDetectionModule>();
    [SerializeField] protected ObstacleDetectionModule chosenDirection;
    [Space(10)]

    [Header("Set Up")]
    [SerializeField] protected Transform enemyTransform;
    [SerializeField] protected ObstacleDetectionModule[] obstacleDetectors;
    [SerializeField] protected EnemyBehavior enemyBehavior;
    [SerializeField] protected AimModule aimDetection;
    [SerializeField] protected LayerMask obstacleLayers;
    [SerializeField] protected int directionTowardsTarget = 0;
    [SerializeField] protected float acceptableDOTForSetTargetDirection = 0.25f;
    [SerializeField] protected float scanDistance = 10.0f;
    //[SerializeField] protected ObstacleDetectionModule fwd;
    //[SerializeField] protected ObstacleDetectionModule fwdRight;
    //[SerializeField] protected ObstacleDetectionModule right;
    //[SerializeField] protected ObstacleDetectionModule bwdRight;
    //[SerializeField] protected ObstacleDetectionModule bwd;
    //[SerializeField] protected ObstacleDetectionModule bwdLeft;
    //[SerializeField] protected ObstacleDetectionModule left;
    //[SerializeField] protected ObstacleDetectionModule fwdLeft;

    public Transform ChosenDirectionTransform => chosenDirection.transform;



    //protected Transform Target => enemyBehavior.CurrentTarget;




        // public returns
    public virtual bool IsLookingTowardsTarget(float acceptableDOTRange)
    {
        Vector3 detectorDirection = obstacleDetectors[0].transform.forward;
        Vector3 towardsTarget = enemyBehavior.CurrentTarget.position - transform.position;
        float dot = aimDetection.DirectionToTargetDOT(detectorDirection, towardsTarget);

        if (dot >= 1f - acceptableDOTRange)
        {
            Debug.Log("Is Looking at Target True, dot: " + dot);
            return true;
        }
        Debug.Log("Is Looking at Target False, dot: " + dot);
        return false;
    }
    public virtual int DetermineDirectionClosestToTarget()
    {
        for (int i = 0; i < obstacleDetectors.Length; i++)
        {
            Vector3 detectorDirection = obstacleDetectors[i].transform.forward;
            Vector3 towardsTarget = enemyBehavior.CurrentTarget.position - transform.position;
            float dot = aimDetection.DirectionToTargetDOT(detectorDirection, towardsTarget);

            if (dot >= 1f - acceptableDOTForSetTargetDirection)
            {
                return i;
            }
        }

        Debug.Log("Closest direction not determined! Returning 0");
        return 0;
    }
    public virtual Vector3 DirectionTowardsTarget()
    {
        int index = DetermineDirectionClosestToTarget();
        return obstacleDetectors[index].transform.forward;
    }



    // Main

    // Phase 1
    public void RotateToTargetAndSetPriorities()
    {
        directionTowardsTarget = 0;

        if (enemyBehavior.CurrentTarget != null)
        {
            RotateTowardsTarget(enemyBehavior.CurrentTarget.position);
            directionTowardsTarget = DetermineDirectionClosestToTarget();
        }
        else
        {
            Debug.Log("Target null!");
            RotateTowardsTarget(enemyBehavior.PlayerPosition);
            directionTowardsTarget = DetermineDirectionClosestToTarget();
        }

        foreach (ObstacleDetectionModule obstacleDetector in obstacleDetectors)
        {
            obstacleDetector.SetPrioDirectionsByObstacles(scanDistance, obstacleLayers);
        }

        AdjustPrioForDirectionAndAdjecents(directionTowardsTarget, 1);
        obstacleDetectors[directionTowardsTarget].AdjustPrioBy(1);

        foreach (ObstacleDetectionModule obstacleDetector in obstacleDetectors)
        {
            obstacleDetector.DebugLine(0.1f);
        }


        if (chosenDirection.Priority <= 0)
        {
            CheckPrioritiesAndRollDirection();
        }
    }
    protected virtual void RotateTowardsTarget(Vector3 targetPos)
    {
        Vector3 relativePos = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        enemyTransform.rotation = Quaternion.Lerp(transform.rotation, rotation, enemyBehavior.RotationSpeed * Time.deltaTime);
    }
    
    protected void AdjustPrioForDirectionAndAdjecents(int indexOfMainDirection, int adjustmentAmount)
    {
        int lowerNeighbor = indexOfMainDirection - 1;
        if (lowerNeighbor < 0)
        {
            lowerNeighbor = obstacleDetectors.Length - 1;
        }

        int higherNeighbor = indexOfMainDirection + 1;
        if (higherNeighbor >= obstacleDetectors.Length)
        {
            higherNeighbor = 0;
        }


        obstacleDetectors[indexOfMainDirection].AdjustPrioBy(adjustmentAmount);
        obstacleDetectors[lowerNeighbor].AdjustPrioBy(adjustmentAmount);
        obstacleDetectors[higherNeighbor].AdjustPrioBy(adjustmentAmount);
    }

        // Phase 2
    public void CheckPrioritiesAndRollDirection()
    {
        Debug.Log("Rolling New Direction From Priorities");
        potentialChoices.Clear();

        foreach (ObstacleDetectionModule direction in obstacleDetectors)
        {
            if (direction.Priority > 0)
            {
                for (int i = 0; i < direction.Priority; i++)
                {
                    potentialChoices.Add(direction);
                }
            }
        }

        if (potentialChoices.Count > 0)
        {
            int roll = Random.Range(0, potentialChoices.Count);
            chosenDirection = potentialChoices[roll];
        }
        else
        {
            Debug.Log("No choices are acceptable, moving forward");
            chosenDirection = obstacleDetectors[0];
        }
    }
    public void MoveTowardsChosenDirection(float moveSpeed)
    {
        Vector3 stepForwardPos = chosenDirection.transform.forward * enemyBehavior.CombatSpeed * Time.deltaTime;
        enemyTransform.position += stepForwardPos;
    }
    

   











    //public void NewPrioScan(bool isTryingToMoveFwd)
    //{
    //    foreach (ObstacleDetectionModule obstacleDetector in obstacleDetectors)
    //    {
    //        obstacleDetector.SetNewPrio(1);
    //    }


    //    if (isTryingToMoveFwd)
    //    {
    //        AdjustPrioForDirectionAndAdjecents(0, 3);
    //        AdjustPrioForDirectionAndAdjecents(4, -3);
    //    }
    //    else
    //    {
    //        AdjustPrioForDirectionAndAdjecents(0, -3);
    //        AdjustPrioForDirectionAndAdjecents(4, 3);
    //    }


    //    // Adjustments for obstacles
    //    for (int i = 0; i < obstacleDetectors.Length; i++)
    //    {
    //        if (obstacleDetectors[i].ObstacleScanDistance() <= objectRangeToLowerPrio1)
    //        {
    //            obstacleDetectors[i].AdjustPrioBy(-1);

    //            if (obstacleDetectors[i].ObstacleScanDistance() <= objectRangeToLowerPrio2)
    //            {
    //                //obstacleDetectors[i].AdjustPrioBy(-1);
    //                AdjustPrioForDirectionAndAdjecents(i, -1);

    //                if (obstacleDetectors[i].ObstacleScanDistance() <= objectRangeToLowerPrio3)
    //                {
    //                    obstacleDetectors[i].AdjustPrioBy(-1);
    //                    AdjustPrioForDirectionAndAdjecents(i, -1);


    //                    int lowerNeighbor = i - 1;
    //                    if (lowerNeighbor < 0)
    //                    {
    //                        lowerNeighbor = obstacleDetectors.Length - 1;
    //                    }

    //                    int higherNeighbor = i + 1;
    //                    if (higherNeighbor >= obstacleDetectors.Length)
    //                    {
    //                        higherNeighbor = 0;
    //                    }
    //                    AdjustPrioForDirectionAndAdjecents(lowerNeighbor, -1);
    //                    AdjustPrioForDirectionAndAdjecents(higherNeighbor, -1);
    //                }
    //            }
    //        }

    //        obstacleDetectors[i].DrawMyPrioLength();
    //    }

    //    CheckPrioritiesAndRollDirection();
    //}







    //public Vector3 DecideDirection()
    //{
    //    highestPrioDirections.Clear();

    //    int currentHighestPrio = 0;
    //    ObstacleDetector tempHighestPrio;

    //    foreach (ObstacleDetector direction in obstacleDetectors)
    //    {
    //        direction.ScanForObstaclesAndAdjustPrio();

    //        if (direction.priority > currentHighestPrio)
    //        {
    //            Debug.Log("New highest prio found, clearing and adding to list");

    //            highestPrioDirections.Clear();
    //            currentHighestPrio = direction.priority;
    //            tempHighestPrio = direction;
    //            highestPrioDirections.Add(direction);
    //        }
    //        else if (direction.priority == currentHighestPrio)
    //        {
    //            Debug.Log("Same prio as highest, adding to list");

    //            //highestPrioDirections.Add(direction);

    //            //currentHighestPrio = direction.priority;
    //            //tempHighestPrio = direction;

    //            highestPrioDirections.Add(direction);
    //        }
    //    }

    //    int roll = Random.Range(0, highestPrioDirections.Count);
    //    ObstacleDetector rolledDirection = highestPrioDirections[roll];
    //    Debug.Log("HighestPrio list completed with " +  highestPrioDirections.Count + " potential choices. Rolled choice: " + rolledDirection.name);

    //    return rolledDirection.transform.forward;
    //}
}
