using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    [SerializeField] protected PlayerHP playerHP;
    [SerializeField] protected float invulnerabilityDuration = 0.3f;
    [SerializeField] protected float timer = 0.0f;
    [SerializeField] protected float dodgeDuration = 0.2f;
    [SerializeField] protected float dodgeLength = 2.0f;
    protected float dodgeVelocity => dodgeLength / dodgeDuration;



    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        timer = 0.0f;
        playerHP.DodgeRollInvulnerability(invulnerabilityDuration);
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void Step()
    {
        base.Step();

        Timer();
        DodgeMovement();
    }

    protected void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= dodgeDuration)
        {
            playerStateMachine.SwitchToDefaultState();
        }
    }

    protected virtual void DodgeMovement()
    {
        float movementStep = dodgeVelocity * Time.deltaTime;
        transform.position += transform.forward * movementStep;
    }
}
