using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_WildSpiral : AttackState_Base
{
    [SerializeField] protected EnemyBehavior_Sawblade enemyBehaviorSawblade;


    [SerializeField] protected float sequenceDuration = 3.0f;
    [SerializeField] protected float rotationSpeed = 30.0f;
    [SerializeField] protected float rotationRadius = 3.0f;
    [SerializeField] protected float radiusOffset = 1.0f;
    [SerializeField] protected Vector3 rotationPoint;
    //[SerializeField] protected GameObject anchorPrefab;
    //[SerializeField] protected GameObject anchorInstance;



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        if (enemy is EnemyBehavior_Sawblade)
        {
            Debug.Log("Entering AttackState_WildSpiral");

            enemyBehaviorSawblade = enemy as EnemyBehavior_Sawblade;
            timer = 0.0f;

            enemyBehaviorSawblade.Rotator.SetRotationSpeed(3);
        }
        else
        {
            Debug.Log("Attached AttackState is not AttackState_Sawblade!");
        }
        
        
        GenerateRotationPoint();
    }
    public override void Step()
    {
        base.Step();

        if (timer <= sequenceDuration)
        {
            Attacking();
        }
        else
        {
            enemyBehavior.SwitchToCooldownState();
        }
    }
    protected virtual void Attacking()
    {
        timer += Time.deltaTime;
        RotateAroundAnchorStep();
    }



    protected virtual void GenerateRotationPoint()
    {
        rotationRadius = enemyBehavior.DistanceToTarget;
        float offsetRoll = Random.Range(-radiusOffset, radiusOffset);
        float currentRadius = rotationRadius + offsetRoll;

        Vector3 dirTowardsTarget = enemyBehavior.Movement.DirectionTowardsTarget();
        Vector3 newPoint = dirTowardsTarget * currentRadius;
        rotationPoint = transform.position + newPoint;

        Debug.Log("New RotationSequence set for WildSpiral, radius: " + currentRadius + ", axis: " + newPoint);
    }


    protected virtual void RotateAroundAnchorStep()
    {
        transform.RotateAround(rotationPoint, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
