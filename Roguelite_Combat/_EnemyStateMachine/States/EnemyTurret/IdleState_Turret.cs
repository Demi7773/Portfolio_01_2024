using UnityEngine;

public class IdleState_Turret : IdleState_Base
{
    [Header("Turret Specific")]
    //[SerializeField] protected EnemyBehavior_Turret enemyBehaviorTurret;
    [SerializeField] protected float lookAroundIdleSpeed = 3.0f;
    [SerializeField] protected float lookAroundRadius;

    protected float startingYRotation;



    // Overriden Behavior Logic
    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        startingYRotation = transform.eulerAngles.y;

        //enemyBehaviorTurret = enemy as EnemyBehavior_Turret;
        //Debug.Log("Entering CombatState_Turret");
    }
    
    protected override void ContinueIdleBehavior()
    {
        LookAroundStep();
    }



    // Behavior
    protected virtual void LookAroundStep()
    {
        float yValue = Mathf.Sin(Time.time * lookAroundIdleSpeed) * lookAroundRadius;
        //Quaternion newRot = Quaternion.Euler(new Vector3(transform.rotation.x, yValue, transform.rotation.z));
        Quaternion newRot = Quaternion.Euler(new Vector3(transform.rotation.x, startingYRotation + yValue, transform.rotation.z));
        transform.rotation = newRot;
    }

}
