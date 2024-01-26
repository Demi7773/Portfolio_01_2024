using UnityEngine;

public class IdleState_Base : _EnemyState
{
    [SerializeField] protected float scanTimer;
    [SerializeField] protected float timeBetweenMovementScans;



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        Debug.Log("Entering IdleState");
    }
    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting IdleState");
    }
    public override void Step()
    {
        base.Step();

        if (!AggroCheck())
        {
            ContinueIdleBehavior();
        }
        else
        {
            Debug.Log("Detected player!");
            enemyBehavior.SwitchToCombatState();
        }
    }



    protected virtual void ContinueIdleBehavior()
    {
        ScanSurroundingsTimer();
    }

    protected virtual void ScanSurroundingsTimer()
    {
        scanTimer += Time.deltaTime;
        if (scanTimer > timeBetweenMovementScans)
        {
            enemyBehavior.Movement.CheckPrioritiesAndRollDirection();
            scanTimer = 0.0f;
        }
    }

    protected virtual bool AggroCheck()
    {
        if (Vector3.Distance(transform.position, enemyBehavior.PlayerPosition) <= enemyBehavior.DetectionRadius)
            return true;

        return false;
    }

}
