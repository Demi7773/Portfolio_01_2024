using UnityEngine;

public class PlayerControlsUpgraded : MonoBehaviour
{
        [Header("Dependencies")]
    [SerializeField] protected PlayerStats stats;
    [SerializeField] protected PlayerStateMachine stateMachine;
    [SerializeField] protected PlayerStamina staminaScript;

        [Space(20)]
        [Header("Stats - Set in inspector")]
    [SerializeField] protected float lockInputsTimer = 0.0f;
    [SerializeField] protected float attackLockInputs = 0.5f;
    [SerializeField] protected float timeBetweenAttacks = 1.0f;
    [SerializeField] protected float attackTimer = 0.0f;
    [SerializeField] protected float attackStaminaCost = 10.0f;
    [SerializeField] protected float dodgeRollStaminaCost;


        // stats from dependencies
    [SerializeField] protected float moveSpeed => stats.MoveSpeed;

    [SerializeField] protected bool hasStaminaForAttack => staminaScript.HasEnoughStaminaForAction(attackStaminaCost);
    [SerializeField] protected bool hasStaminaForDodgeRoll => staminaScript.HasEnoughStaminaForAction(dodgeRollStaminaCost);







    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;
        lockInputsTimer -= Time.deltaTime;

        if (CanAct())
        {
            Movement();

            if (CanAttack())
            {
                Combat();
            }
        }
    }



    protected virtual bool CanAct()
    {
        if (lockInputsTimer <= 0.0f)
        { return true; }
        else
        { return false; }
    }
    protected virtual bool CanAttack()
    {
        if (attackTimer <= 0.0f)
            return true;
        else
            return false;
    }

    protected virtual void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputTotal = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (inputTotal != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(inputTotal);
            transform.position += inputTotal * moveSpeed * Time.deltaTime;
        }

        //transform.position += transform.forward * moveSpeed * Timer.deltaTime;
    }

    protected virtual void Combat()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            lockInputsTimer = attackLockInputs;
            attackTimer = timeBetweenAttacks;
            //attack.UseAttack();
        }
    }
}
