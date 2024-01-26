using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] protected List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] protected int currentTarget = 0;

    [SerializeField, Range(1.0f, 100.0f)] protected float playerDetectionRadius = 30f;
    [SerializeField, Range(1.0f, 30.0f)] protected float patrolPointReachedRange = 5f;


    [SerializeField] private bool stoppedAtPatrolPoint = false;

    


    public override void EnterState(EnemyBehavior enemy, GameObject playerGO)
    {
        base.EnterState(enemy, playerGO);
        LookForNearestPatrolPoint();
    }
    public override void Step()
    {
        if (DistanceFromPlayer() > playerDetectionRadius)
        {
            CheckSituation();
        }
        else
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        enemyBehavior.SwitchToCombatState();
        base.ExitState();
    }



    protected void LookForNearestPatrolPoint()
    {
        float shortestDistance = 1000f;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float distanceToComparedPoint = DistanceToPatrolPoint(patrolPoints[i]);
            if (distanceToComparedPoint < shortestDistance)
            {
                shortestDistance = distanceToComparedPoint;
                currentTarget = i;
                Debug.Log("Nearest Patrol Point found, new target: " + currentTarget);
            }
        }
    }

    protected void CheckSituation()
    {
        if (!stoppedAtPatrolPoint)
        {
            if (DistanceToPatrolPoint(patrolPoints[currentTarget]) > patrolPointReachedRange)
            {
                ContinuePatrol();
            }
            else
            {
                PatrolPointReached();
            }
        }
        else
        {
            Debug.Log("Stopped at PatrolPoint");
        }
    }

    protected void ContinuePatrol()
    {
        RotateTowardsTargetPositionWithRotateSpeed(patrolPoints[currentTarget]/*TowardsPatrolPointDirection()*/);

        MoveForwardStep();
    }

    protected void PatrolPointReached()
    {
        currentTarget++;
        if (currentTarget > patrolPoints.Count)
        {
            currentTarget = 0;
        }
        Debug.Log("Patrol Point Reached, new Target: " + currentTarget);
        StartCoroutine("StopAtPatrolPoint");
    }
    IEnumerator StopAtPatrolPoint()
    {
        stoppedAtPatrolPoint = true;
        yield return new WaitForSeconds(enemyBehavior.DelayAtPatrolPoint);
        stoppedAtPatrolPoint = false;
    }



    protected float DistanceToPatrolPoint(Transform patrolPoint)
    {
        return Vector3.Distance(transform.position, patrolPoint.position);
    }
    protected virtual Vector3 TowardsPatrolPointDirection()
    {
        Vector3 direction = patrolPoints[currentTarget].transform.position - transform.position;
        //Debug.DrawRay(transform.position, direction, color: Color.blue);
        return direction;
    }
}
