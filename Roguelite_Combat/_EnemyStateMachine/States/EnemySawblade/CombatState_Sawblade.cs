using UnityEngine;

public class CombatState_Sawblade : CombatState_Base
{
    //[Header("Turret Specific")]
    //[SerializeField] protected AimDetection aimDetection;

    //protected float movementDecisionTimer = 0.0f;
    //[SerializeField] protected float timeBeetwenMovementDecisions = 0.1f;




    // Sawblade Override Behaviors

    protected override void Move()
    {
        enemyBehavior.Movement.MoveTowardsChosenDirection(enemyBehavior.IdleSpeed);
    }









    // Old
    //protected override void DecideBehavior()
    //{
    //    //movementDecisionTimer += Time.deltaTime;


    //    //if (!CanAttack(enemyBehavior.PlayerPosition, enemyBehavior.TargetLayers))
    //    //{
    //    //    CantAttackBehavior(enemyBehavior.PlayerPosition);
    //    //}
    //    //else
    //    //{
    //    //    Attack();
    //    //}
    //}



    // Check Conditions
    //protected override bool CanAttack(Vector3 targetPos, LayerMask targetLayer)
    //{
    //    if (IsAimingAtTarget(targetLayer))
    //    {
    //        return base.CanAttack(targetPos, targetLayer);
    //    }
    //    return false;
    //}
    //protected virtual bool IsAimingAtTarget(LayerMask targetLayer)
    //{
    //    return aimDetection.IsAimingAtTarget(enemyBehavior.AttemptAttackRange, targetLayer);
    //}


    //protected virtual void CantAttackBehavior(Vector3 targetPos)
    //{
    //    if (Vector3.Distance(transform.position, targetPos) <= enemyBehavior.DetectionRadius)
    //    {
    //        RotateTowardsTarget(targetPos);
    //        backToIdleTimer = 0.0f;
    //        Debug.Log("Player out of Turret AttackRange but in DetectionRadius, rotating");

    //        if (movementDecisionTimer > timeBeetwenMovementDecisions)
    //        {
    //            DecideMovement(true);
    //            movementDecisionTimer = 0.0f;
    //        }

    //        MoveStep(enemyBehavior.ChosenMoveDirection);
    //    }
    //    else
    //    {
    //        backToIdleTimer += Time.deltaTime;
    //        //Debug.Log("Player out of Detection range tick");

    //        SwitchToIdleCheck();
    //    }
    //}


    //protected virtual void DecideMovement(bool wantsToMoveToPlayer)
    //{
    //    enemyBehavior.MovementModule.NewPrioScan(wantsToMoveToPlayer);
    //}

    //protected virtual void MoveStep(Vector3 direction)
    //{
    //    Vector3 stepForwardPos = direction * enemyBehavior.CombatSpeed;
    //    transform.position += stepForwardPos * Time.deltaTime;
    //}
}
