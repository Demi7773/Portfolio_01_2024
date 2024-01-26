using UnityEngine;

public class CombatState_Melee : CombatState_Base
{
    //[SerializeField] protected float timeSinceLastAttack = 100.0f;
    //protected float scanTimer = 0.0f;
    //[SerializeField] protected float timeBetweenScans = 0.1f;



    //public override void EnterState(EnemyBehavior enemy)
    //{
    //    base.EnterState(enemy);
    //    Debug.Log("Entering CombatState_Base");
    //    //timeSinceLastAttack = 100.0f;
    //}
    //public override void ExitState()
    //{
    //    base.ExitState();
    //    Debug.Log("Exiting CombatState_Base");
    //}

    //public override void Step()
    //{
    //    scanTimer += Time.deltaTime;
    //    transform.LookAt(enemyBehavior.PlayerPosition);
    //    base.Step();
    //}



    //protected override void Attack()
    //{
    //    base.Attack();
    //}

    //protected virtual void CantAttackBehavior()
    //{
    //    if (scanTimer > timeBetweenScans)
    //    {
    //        DecideMovement(true);
    //        scanTimer = 0.0f;
    //    }

    //    MoveStep();
    //}
    //protected virtual void AttackOnCooldownBehavior()
    //{
    //    if (scanTimer > timeBetweenScans)
    //    {
    //        DecideMovement(false);
    //        scanTimer = 0.0f;
    //    }

    //    MoveStep();
    //}



    //protected virtual void DecideMovement(bool wantsToMoveToPlayer)
    //{
    //    //enemyBehavior.MovementModule.NewPrioScan(wantsToMoveToPlayer);
    //}

    //protected virtual void MoveStep()
    //{
    //    Vector3 stepForwardPos = enemyBehavior.MovementModule.chosenDirection.forward * enemyBehavior.CombatSpeed;
    //    transform.position += stepForwardPos * Time.deltaTime;
    //}
}
