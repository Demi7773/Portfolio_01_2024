using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerState currentState;
    [SerializeField] private PlayerState defaultState;

    [Space(20)]
    [SerializeField] private PlayerMoveState moveState;
    [SerializeField] private PlayerAttackState attackState;
    [SerializeField] private PlayerDodgeState dodgeState;
    [SerializeField] private PlayerStaggerState staggerState;

    [Space(20)]
    [Header("Timer Stats")]
    [SerializeField] private float timeSinceLastAttack = 100.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    [SerializeField] private float timeSinceLastDodge = 100.0f;
    [SerializeField] private float dodgeCooldown = 1.0f;




    public bool CanAttackAgain()
    {
        if (timeSinceLastAttack >= attackCooldown)
            return true;

        return false;
    }
    public bool CanRollAgain()
    {
        if (timeSinceLastDodge >= dodgeCooldown)
            return true;

        return false;
    }



    private void Update()
    {
        TimersTick();
        currentState.Step();
    }
    private void TimersTick()
    {
        timeSinceLastDodge += Time.deltaTime;
        timeSinceLastAttack += Time.deltaTime;
    }



    private void Awake()
    {
        currentState = defaultState;
        currentState.EnterState(this);
        timeSinceLastDodge = 100.0f;
        timeSinceLastAttack = 100.0f;
    }

    public void SwitchToDefaultState()
    {
        currentState.ExitState();
        currentState = defaultState;
        currentState.EnterState(this);
    }
    public void SwitchToMoveState()
    {
        currentState.ExitState();
        currentState = moveState;
        currentState.EnterState(this);
    }
    public void SwitchToAttackState()
    {
        currentState.ExitState();
        currentState = attackState;
        currentState.EnterState(this);
        timeSinceLastAttack = 0.0f;
    }
    public void SwitchToDodgeState()
    {
        currentState.ExitState();
        currentState = dodgeState;
        currentState.EnterState(this);
        timeSinceLastDodge = 0.0f;
    }
    public void SwitchToStaggerState()
    {
        currentState.ExitState();
        currentState = staggerState;
        currentState.EnterState(this);
    }
}
