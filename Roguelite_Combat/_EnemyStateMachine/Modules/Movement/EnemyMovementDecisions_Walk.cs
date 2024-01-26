using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDecisions_Walk : MonoBehaviour
{
    //[SerializeField] private List<ObstacleDetector> highestPrioDirections = new List<ObstacleDetector>();
    [SerializeField] private List<Transform> potentialChoices = new List<Transform>();


    [SerializeField] private ObstacleDetector[] enemyDirections;
    [SerializeField] private ObstacleDetector fwd;
    [SerializeField] private ObstacleDetector fwdRight;
    [SerializeField] private ObstacleDetector right;
    [SerializeField] private ObstacleDetector bwdRight;
    [SerializeField] private ObstacleDetector bwd;
    [SerializeField] private ObstacleDetector bwdLeft;
    [SerializeField] private ObstacleDetector left;
    [SerializeField] private ObstacleDetector fwdLeft;

    public Transform chosenDirection;

    [SerializeField] private float objectRangeToLowerPrio1 = 6.0f;
    [SerializeField] private float objectRangeToLowerPrio2 = 3.0f;
    [SerializeField] private float objectRangeToLowerPrio3 = 1.0f;



    private void Update()
    {
        //DecideDirection();
    }

    public void NewPrioScan(bool isTryingToMoveFwd)
    {

        foreach (ObstacleDetector obstacleDetector in enemyDirections)
        {
            obstacleDetector.SetNewPrio(1);
        }


        if (isTryingToMoveFwd)
        {
            AdjustPrioForDirectionAndAdjecents(0, 3);
            AdjustPrioForDirectionAndAdjecents(4, -3);
        }
        else
        {
            AdjustPrioForDirectionAndAdjecents(0, -3);
            AdjustPrioForDirectionAndAdjecents(4, 3);
        }


        // Adjustments for obstacles
        for(int i = 0; i < enemyDirections.Length; i++)
        {
            if (enemyDirections[i].ObstacleScanDistance() <= objectRangeToLowerPrio1)
            {
                enemyDirections[i].AdjustPrioBy(-1);

                if (enemyDirections[i].ObstacleScanDistance() <= objectRangeToLowerPrio2)
                {
                    //obstacleDetectors[i].AdjustPrioBy(-1);
                    AdjustPrioForDirectionAndAdjecents(i, -1);

                    if (enemyDirections[i].ObstacleScanDistance() <= objectRangeToLowerPrio3)
                    {
                        enemyDirections[i].AdjustPrioBy(-1);
                        AdjustPrioForDirectionAndAdjecents(i, -1);
                        

                        int lowerNeighbor = i - 1;
                        if (lowerNeighbor < 0)
                        {
                            lowerNeighbor = enemyDirections.Length - 1;
                        }

                        int higherNeighbor = i + 1;
                        if (higherNeighbor >= enemyDirections.Length)
                        {
                            higherNeighbor = 0;
                        }
                        AdjustPrioForDirectionAndAdjecents(lowerNeighbor, -1);
                        AdjustPrioForDirectionAndAdjecents(higherNeighbor, -1);
                    }
                }
            }

            enemyDirections[i].DrawMyPrioLength();
        }


        CheckPrioritiesAndRollDirection();
    }

    private void AdjustPrioForDirectionAndAdjecents(int indexOfMainDirection, int adjustmentAmount)
    {
        int lowerNeighbor = indexOfMainDirection - 1;
        if (lowerNeighbor < 0) 
        {
            lowerNeighbor = enemyDirections.Length - 1;
        }

        int higherNeighbor = indexOfMainDirection + 1;
        if (higherNeighbor >= enemyDirections.Length)
        {
            higherNeighbor = 0;
        }


        enemyDirections[indexOfMainDirection].AdjustPrioBy(adjustmentAmount);
        enemyDirections[lowerNeighbor].AdjustPrioBy(adjustmentAmount);
        enemyDirections[higherNeighbor].AdjustPrioBy(adjustmentAmount);
    }

    public void CheckPrioritiesAndRollDirection()
    {
        potentialChoices.Clear();

        foreach (ObstacleDetector direction in enemyDirections)
        {
            if (direction.Priority > 0)
            {
                for (int i = 0; i < direction.Priority; i++)
                {
                    potentialChoices.Add(direction.transform);
                }
            }
        }

        if (potentialChoices.Count > 0)
        {
            int roll = Random.Range(0, potentialChoices.Count);
            Transform rolledTransform = potentialChoices[roll].transform;
            chosenDirection = rolledTransform;
        }
        else
        {
            Debug.Log("No choices are acceptable, moving forward");
            chosenDirection = fwd.transform;
        }
    }






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
