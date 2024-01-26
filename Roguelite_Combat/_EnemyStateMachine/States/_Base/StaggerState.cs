using UnityEngine;

public class StaggerState : _EnemyState
{
    [SerializeField] protected float timeSinceStart = 0.0f;

    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);
        Debug.Log("Entering StaggerState");

        enemyBehavior.ShakeEffect.Shake();
        timeSinceStart = 0.0f;
    }
    public override void ExitState()
    {
        base.ExitState();

        enemyBehavior.ShakeEffect.ResetSize();
        Debug.Log("Exiting StaggerState");
    }


    public override void Step()
    {
        base.Step();

        timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= enemyBehavior.BaseStaggerDuration)
        {
            enemyBehavior.SwitchToCombatState();
        }
    }
}
