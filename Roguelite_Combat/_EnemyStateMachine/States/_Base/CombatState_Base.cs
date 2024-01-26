using UnityEngine;

public class CombatState_Base : _EnemyState
{
    [Header("Attack stats")]
    [SerializeField] protected float acceptableAimDOTRange = 0.1f;
    [SerializeField] protected float timeSinceLastAttack = 100.0f;
    [Header("Out of Combat")]
    [SerializeField] protected float backToIdleTimer = 0.0f;
    [SerializeField] protected float timeBeforeSwitchBackToIdle = 3.0f;
    [Header("Scan for Movement Settings")]
    [SerializeField] protected float scanTimer;
    [SerializeField] protected float timeBetweenMovementScans = 0.2f;
   


        // target set to player when entering combat, change for other targets if necessary
    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        enemyBehavior.SetNewTarget(enemyBehavior.Player.transform);
        timeSinceLastAttack = 100.0f;
        backToIdleTimer = 0.0f;
        scanTimer = 0.0f;
        Debug.Log("Entering CombatState");
    }
    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting CombatState");
    }
    public override void Step()
    {
        base.Step();
        timeSinceLastAttack += Time.deltaTime;
        DecideBehavior();
    }
    protected virtual void DecideBehavior()
    {
        if (CanMove())
        {
            ScanSurroundingsTimer();

            if (CanSeeTarget())
            {
                //Debug.Log("CanSeeTarget -> CombatBehavior");
                InCombat();
            }
            else
            {
                LookForTarget();
            }
        }
    }

    protected virtual void ScanSurroundingsTimer()
    {
        scanTimer += Time.deltaTime;
        if (scanTimer > timeBetweenMovementScans)
        {
            enemyBehavior.Movement.CheckPrioritiesAndRollDirection();
        }
    }





    // *********************     OVERRIDE WHERE NEEDED    *******************************

    // Behaviors
    protected virtual void InCombat()
    {
        enemyBehavior.Movement.RotateToTargetAndSetPriorities();

        if (!CanAttack())
        {
            Move();
        }
        else
        {
            Attack();
        }
    }
    protected virtual void LookForTarget()
    {
        backToIdleTimer += Time.deltaTime;
        SwitchToIdleCheck();

        Debug.Log("Looking for Target");
    }



    protected virtual void Attack()
    {
        timeSinceLastAttack = 0.0f + enemyBehavior.AttackCooldownDuration;
        enemyBehavior.SwitchToAttackState();
    }
    protected virtual void Move()
    {
        enemyBehavior.Movement.MoveTowardsChosenDirection(enemyBehavior.CombatSpeed);
    }




        // Combat Conditions

    protected virtual bool CanSeeTarget(/*Vector3 targetPos*/)
    {
        return Vector3.Distance(transform.position, enemyBehavior.CurrentTarget.position) <= enemyBehavior.DetectionRadius;
    }
    protected virtual void SwitchToIdleCheck()
    {
        if (backToIdleTimer >= timeBeforeSwitchBackToIdle)
        {
            Debug.Log("Target out of range too long -> SwitchToDefaultState");
            enemyBehavior.SwitchToDefaultState();
        }
    }



    protected virtual bool CanMove()
    {
        return true;
    }



    protected virtual bool CanAttack()
    {
        if (AttackCooldownFinished() && IsTargetInAttackRange() && IsAimingAtTarget())
        {
            return true;
        }
        return false;
    }
    protected virtual bool AttackCooldownFinished()
    {
        return timeSinceLastAttack >= enemyBehavior.TimeBetweenAttacks;
    }
    protected virtual bool IsTargetInAttackRange()
    {
        return Vector3.Distance(transform.position, enemyBehavior.CurrentTarget.position) <= enemyBehavior.AttemptAttackRange;
    }
    protected virtual bool IsAimingAtTarget()
    {
        return enemyBehavior.Movement.IsLookingTowardsTarget(acceptableAimDOTRange);
    }
}
