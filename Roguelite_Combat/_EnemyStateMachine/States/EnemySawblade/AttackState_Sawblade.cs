using UnityEngine;

public class AttackState_Sawblade : AttackState_Base
{
    [Space(20)]
    [Header("Sawblade Specific")]
    [SerializeField] protected EnemyBehavior_Sawblade enemyBehaviorSawblade;
    [Header("Attack sequence")]
    [SerializeField] protected int attacksMade = 0;
    [SerializeField] protected int numberOfAttacksInSequence = 3;
    [Space(10)]
    [Header("Mini Cooldown")]
    [SerializeField] protected float startNextAttackTimer = 0.0f;
    [SerializeField] protected float timeBetweenAttacks = 0.5f;
    [Header("ChargeUp")]
    [SerializeField] protected bool isChargingAttack = false;
    [SerializeField] protected float chargeUpTimer = 0.0f;
    [SerializeField] protected float chargeUpDuration = 0.3f;
    [Header("Aim")]
    [SerializeField] protected float aimOffsetForXZPosition = 1.0f;
    [SerializeField] protected Vector3 targetPosition;
    [Header("Attacking Behavior")]
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected float attackingTimer = 0.0f;
    [SerializeField] protected float attackInstanceDuration = 0.2f;
    [SerializeField] protected float attackMoveForwardSpeed = 10.0f;




   


    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        if (enemy is EnemyBehavior_Sawblade)
        {
            //Debug.Log("Entering AttackState_Sawblade");

            enemyBehaviorSawblade = enemy as EnemyBehavior_Sawblade;
            attacksMade = 0;
            startNextAttackTimer = 0.0f;
            isChargingAttack = false;
            chargeUpTimer = 0.0f;
            isAttacking = false;
            attackingTimer = 0.0f;


            enemyBehaviorSawblade.Rotator.SetRotationSpeed(2);
        }
        else
        {
            Debug.Log("Attached AttackState is not AttackState_Sawblade!");
        }
    }
    public override void ExitState()
    {
        base.ExitState();

        //Debug.Log("Exiting AttackState_Sawblade");
    }

    public override void Step()
    {
        base.Step();

        if (isAttacking)
        {
            Attacking();
        }
        else if (isChargingAttack)
        {
            // add slow rotation and RotationCheck
            ChargingAttack();
        }
        else
        {
            // right now stands dazed until next attack, add vfx/animation
            CoolingDown();
        }
    }



        // Attacking - add curve for movespeed
    protected virtual void Attacking()
    {
        attackingTimer += Time.deltaTime;

        if (attackingTimer < attackInstanceDuration)
        {
            transform.position += transform.forward * attackMoveForwardSpeed * Time.deltaTime;
        }
        else
        {
            AttackCompleted();
        }
    }
    protected virtual void AttackCompleted()
    {
        isAttacking = false;
        attacksMade++;

        if (attacksMade > numberOfAttacksInSequence)
        {
            enemyBehaviorSawblade.Rotator.SetRotationSpeed(1);
            enemyBehaviorSawblade.SwitchToCooldownState();
        }
        else
        {
            enemyBehaviorSawblade.Rotator.SetRotationSpeed(2);
        }  
    }



        // ChargingAttack - add slow rotation and RotationCheck here
    protected virtual void ChargingAttack()
    {
        chargeUpTimer += Time.deltaTime;
        ApproximatePlayerAndSetTargetPosition();
        RotateTowardsTarget();


        if (IsAttackCharged() && IsRotationAdequate())
        {
            isAttacking = true;
            isChargingAttack = false;
            chargeUpTimer = 0.0f;
        }

    }
    protected virtual void ApproximatePlayerAndSetTargetPosition()
    {
        float xOffset = Random.Range(-aimOffsetForXZPosition, aimOffsetForXZPosition);
        float zOffset = Random.Range(-aimOffsetForXZPosition, aimOffsetForXZPosition);
        Vector3 playerPos = enemyBehaviorSawblade.PlayerPosition;
        Vector3 approximation = new Vector3(playerPos.x + xOffset, playerPos.y, playerPos.z + zOffset);

        targetPosition = approximation;
    }
    protected virtual void RotateTowardsTarget()
    {
        //transform.LookAt(enemyBehavior.PlayerPosition);
        Vector3 towardsTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(towardsTarget, transform.up);

        //float rotateAmount = rotationSpeed * Time.deltaTime;
        //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateAmount);
        //transform.rotation = newRotation;

        transform.rotation = targetRotation;
    }
    protected virtual bool IsAttackCharged()
    {
        if (chargeUpTimer >= chargeUpDuration)
            return true;
        return false;
    }
        // temp
    protected virtual bool IsRotationAdequate()
    {
        return true;
    }



        // Cooling Down
    protected virtual void CoolingDown()
    {
        startNextAttackTimer += Time.deltaTime;

        if (startNextAttackTimer > timeBetweenAttacks)
        {
            startNextAttackTimer = 0.0f;
            chargeUpTimer = 0.0f;
            isChargingAttack = true;

            enemyBehaviorSawblade.Rotator.SetRotationSpeed(3);
        }
    }
}
