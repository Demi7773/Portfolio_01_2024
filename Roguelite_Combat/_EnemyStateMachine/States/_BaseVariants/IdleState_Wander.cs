using UnityEngine;

public class IdleState_Wander : IdleState_Base
{
    [SerializeField] protected Transform wanderTarget;
    [SerializeField] protected float wanderRadius = 10.0f;
    [SerializeField] protected float acceptableDistanceFromTarget = 2.0f;

    [SerializeField] protected LayerMask terrainLayer;



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        scanTimer = 0.0f;
        SetNewTarget();
        //Debug.Log("Entering IdleState_Wander");
    }



    protected virtual void SetNewTarget()
    {
        wanderTarget.position = GenerateAcceptableDestination();
        enemyBehavior.SetNewTarget(wanderTarget);
    }
    protected virtual Vector3 GenerateAcceptableDestination()
    {
        Vector3 newTargetOffsetByPos = RollTargetPos();

        int safetyCount = 0;
        while (Physics.CheckSphere(newTargetOffsetByPos, 3.0f, terrainLayer))
        {
            newTargetOffsetByPos = RollTargetPos();

            safetyCount++;
            if(safetyCount >= 10)
            {
                Debug.Log("Safety count reached 10! Breaking");
                break;
            }
        }

        return newTargetOffsetByPos;
    }
    protected virtual Vector3 RollTargetPos()
    {
        float newTargetX = Random.Range(-1f, 1f);
        float newTargetZ = Random.Range(-1f, 1f);

        Vector2 newTarget = new Vector2(newTargetX, /*transform.position.y,*/ newTargetZ).normalized * wanderRadius;
        Vector3 newTargetOffsetByPos = new Vector3(transform.position.x + newTarget.x, transform.position.y, transform.position.z + newTarget.y);

        return newTargetOffsetByPos;
    }
    protected virtual bool HasReachedDestination()
    {
        if (Vector3.Distance(transform.position, wanderTarget.position) > acceptableDistanceFromTarget)
        {
            return false;
        }

        return true;
    }



    protected override void ContinueIdleBehavior()
    {
        base.ContinueIdleBehavior();

        if (!HasReachedDestination())
        {
            MoveForwardStep();
        }
        else
        {
            SetNewTarget();
            Debug.Log("New Idle target: " + wanderTarget.transform.position);
        }
    }
    protected virtual void MoveForwardStep()
    {
        enemyBehavior.Movement.RotateToTargetAndSetPriorities();
        enemyBehavior.Movement.MoveTowardsChosenDirection(enemyBehavior.IdleSpeed);
    } 
}
