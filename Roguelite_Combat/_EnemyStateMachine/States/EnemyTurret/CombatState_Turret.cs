
public class CombatState_Turret : CombatState_Base
{


    // Turret Override Behaviors

    protected override void ScanSurroundingsTimer()
    {
        return;
    }

    protected override void Move()
    {
        return;
    }





    //Old
    // Check Conditions Overrides
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


    //protected virtual void RotateTowardsPlayer()
    //{
    //    //transform.LookAt(enemyBehavior.PlayerPosition);
    //    Vector3 towardsPlayer = enemyBehavior.PlayerPosition - transform.position;
    //    Quaternion targetRotation = Quaternion.LookRotation(towardsPlayer, transform.up);

    //    //float rotateAmount = rotationSpeed * Time.deltaTime;
    //    //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateAmount);
    //    //transform.rotation = newRotation;

    //    transform.rotation = targetRotation;
    //}

}
