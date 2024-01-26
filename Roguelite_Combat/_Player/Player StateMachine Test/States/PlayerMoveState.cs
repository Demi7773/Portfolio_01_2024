using UnityEngine;

public class PlayerMoveState : PlayerState
{
    [Header("Dependencies")]
    [SerializeField] protected PlayerStamina staminaScript;

    [Space(20)]
    [Header("Stats - Set in inspector")]
    [SerializeField] protected float attackStaminaCost = 20.0f;
    [SerializeField] protected float dodgeRollStaminaCost = 10.0f;


    // stats from dependencies
    [SerializeField] protected float moveSpeed => stats.MoveSpeed;

    [SerializeField] protected bool hasStaminaForAttack => staminaScript.HasEnoughStaminaForAction(attackStaminaCost);
    [SerializeField] protected bool hasStaminaForDodgeRoll => staminaScript.HasEnoughStaminaForAction(dodgeRollStaminaCost);




    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void Step()
    {
        base.Step();

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckDodgeConditions())
            {
                staminaScript.UseStaminaForAction(dodgeRollStaminaCost);
                playerStateMachine.SwitchToDodgeState();
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (CheckAttackConditions())
            {
                staminaScript.UseStaminaForAction(attackStaminaCost);
                playerStateMachine.SwitchToAttackState();
            }
        }
    }

    protected virtual bool CheckDodgeConditions()
    {
        if (!playerStateMachine.CanRollAgain())
        {
            Debug.Log("Dodge on Cooldown");
            return false;
        }
        else if (!staminaScript.HasEnoughStaminaForAction(dodgeRollStaminaCost))
        {
            Debug.Log("Not enough stamina for Dodge");
            return false;
        }

        return true;
    }
    protected virtual bool CheckAttackConditions()
    {
        if (!playerStateMachine.CanAttackAgain())
        {
            Debug.Log("Player Attack on Cooldown");
            return false;
        }
        else if (!staminaScript.HasEnoughStaminaForAction(attackStaminaCost))
        {
            Debug.Log("Not enough stamina for Attack");
            return false;
        }

        return true;
    }



    protected virtual void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputTotal = new Vector3(horizontalInput, transform.position.y, verticalInput).normalized;

        if (inputTotal != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(inputTotal);
            //transform.position += inputTotal * moveSpeed * Time.deltaTime;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
