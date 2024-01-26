using UnityEngine;

public class AttackState_Base : _EnemyState
{
    [Header("Dependencies")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float timer = 0.0f;



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        timer = 0.0f;
        Debug.Log("Entering AttackState");
    }
    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting AttackState");
    }


    public override void Step()
    {
        base.Step();
    }
}
