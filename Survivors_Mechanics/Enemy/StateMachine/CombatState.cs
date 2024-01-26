using UnityEngine;

public class CombatState : State
{
    [SerializeField] private bool canAttack = true;



    public override void EnterState(EnemyBehavior enemy, GameObject playerGO)
    {
        base.EnterState(enemy, playerGO);
    }
    public override void Step()
    {
        if (DistanceFromPlayer() <= enemyBehavior.AttackRange)
        {
            if (canAttack)
            {
                AttackPlayer();
            }
        }
    }
    public override void ExitState()
    {
        enemyBehavior.SwitchToPatrolState();
        base.ExitState();
    }



    protected void AttackPlayer()
    {

    }
}
