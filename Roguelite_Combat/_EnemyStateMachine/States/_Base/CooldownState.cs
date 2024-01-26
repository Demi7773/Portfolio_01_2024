using UnityEngine;

public class CooldownState : _EnemyState
{
    [SerializeField] protected float timeSinceStart = 0.0f;

    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        Debug.Log("Entering CooldownState");
        timeSinceStart = 0.0f;
    }
    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting CooldownState");
    }



    public override void Step()
    {
        base.Step();

        timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= enemyBehavior.AttackCooldownDuration)
        {
            enemyBehavior.SwitchToCombatState();
        }
    }
}
