using UnityEngine;
using DG.Tweening;

public class EnemyBehaviorMelee : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected EnemyMovementDecisions_Walk movementDecisions;
    [SerializeField] protected FloatingHPBar hpBar;

    public GameObject Player => player;
    public Vector3 PlayerPosition => player.transform.position;
    public LayerMask PlayerLayer => playerLayer;
    public EnemyMovementDecisions_Walk MovementDecisions => movementDecisions;



    [Space(20)]
    [Header("Shake on Damage Animation")]
    [SerializeField] protected Transform model;
    [SerializeField] protected float shakeDuration = 0.2f;
    [SerializeField] protected float shakeStrength = 1.0f;



    [Space(10)]
    [Header("Stats")]
    [SerializeField] protected float currentHP = 30.0f;
    [SerializeField] protected float maxHP = 30.0f;
    [SerializeField] protected float armor = 10.0f;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public float Armor => armor;



    [Space(30)]
    [Header("StateMachine")]
    [SerializeField] protected _EnemyState currentState;
    [SerializeField] protected _EnemyState defaultState;



    [Space(10)]
    [Header("Idle - Wander")]
    [SerializeField] protected IdleState_Base idle;
    [SerializeField] protected float wanderSpeed = 3.0f;
    [SerializeField] protected float wanderRadius = 10.0f;
    [SerializeField] protected float detectionRadius = 5.0f;

    public IdleState_Base Idle => idle;
    public float WanderSpeed => wanderSpeed;
    public float WanderRadius => wanderRadius;
    public float DetectionRadius => detectionRadius;



    [Space(10)]
    [Header("Combat")]
    [SerializeField] protected CombatState_Base combat;
    [SerializeField] protected float combatSpeed = 4.0f;
    [SerializeField] protected float attemptAttackRange = 1.5f;
    [SerializeField] protected float timeBetweenAttacks = 2.0f;

    public CombatState_Base Combat => combat;
    public float CombatSpeed => combatSpeed;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public float AttemptAttackRange => attemptAttackRange;



    [Space(10)]
    [Header("Attack")]
    [SerializeField] protected AttackState_Melee attack;
    //[SerializeField] protected EnemyAttack attackType;
    [SerializeField] protected float attackRange = 1.0f;
    [SerializeField] protected float attackDuration = 0.1f;
    [SerializeField] protected float damage = 10.0f;

    public AttackState_Melee Attack => attack;
    //public EnemyAttack AttackType => attackType;
    public float AttackRange => attackRange;
    public float AttackDuration => attackDuration;
    public float Damage => damage;



    [Space(10)]
    [Header("Cooldown")]
    [SerializeField] protected CooldownState cooldown;
    [SerializeField] protected float attackCooldownDuration = 0.5f;

    public CooldownState Cooldown => cooldown;
    public float AttackCooldownDuration => attackCooldownDuration;



    [Space(10)]
    [Header("Stagger")]
    [SerializeField] protected StaggerState stagger;
    [SerializeField] protected float baseStaggerDuration = 0.3f;

    public StaggerState Stagger => stagger;
    public float BaseStaggerDuration => baseStaggerDuration;





    protected virtual void OnEnable()
    {
        currentHP = maxHP;
    }
    public virtual void SetPlayerReference(GameObject playerRef)
    {
        player = playerRef;
    }


    //public virtual void SwitchToDefaultState()
    //{
    //    currentState.ExitState();
    //    currentState = defaultState;
    //    currentState.EnterState(this);
    //}

    //public virtual void SwitchToIdleState()
    //{
    //    currentState.ExitState();
    //    currentState = idle;
    //    currentState.EnterState(this);
    //}
    //public virtual void SwitchToCombatState()
    //{
    //    currentState.ExitState();
    //    currentState = combat;
    //    currentState.EnterState(this);
    //}
    //public virtual void SwitchToAttackState()
    //{
    //    currentState.ExitState();
    //    currentState = attack;
    //    currentState.EnterState(this);
    //}
    //public virtual void SwitchToCooldownState()
    //{
    //    currentState.ExitState();
    //    currentState = cooldown;
    //    currentState.EnterState(this);
    //}
    //public virtual void SwitchToStaggerState()
    //{
    //    currentState.ExitState();
    //    currentState = stagger;
    //    currentState.EnterState(this);
    //}



    //protected virtual void Start()
    //{
    //    currentState = defaultState;
    //    currentState.EnterState(this);
    //}
    protected virtual void Update()
    {
        currentState.Step();
    }



    // HP and Death
    public float HPRatio()
    {
        return currentHP / maxHP;
    }
    public virtual void GetHitFor(float dmgAmount)
    {
        float reducedDmgAmount = ApplyDamageReduction(dmgAmount);
        float newHP = currentHP - reducedDmgAmount;

        model.DOShakeScale(shakeDuration, shakeStrength);
        currentHP = Mathf.Clamp(newHP, 0.0f, maxHP);
        hpBar.HPBarUpdate(HPRatio());

        if (currentHP > 0.0f)
        {
            //SwitchToStaggerState();
            //if (currentState is IdleState_Wander)
            //{
            //    SwitchToStaggerState();
            //} 
        }
        else
        {
            Death();
        }
    }
    protected virtual float ApplyDamageReduction(float dmgAmount)
    {
        // Diminishing returns on armor values closer to 100
        // Current system should be:
        // armor = 1 -> dmgReduction = 10%
        // armor = 10 -> dmgReduction = 31.62277660168379%
        // armor = 20 -> dmgReduction = 44.72135954999579%
        // armor = 30 -> dmgReduction = 54.77225575051661%
        // armor = 40 -> dmgReduction = 63.24555320336759%
        // armor = 50 -> dmgReduction = 70.71067811865475%
        // armor = 60 -> dmgReduction = 77.45966692414834%
        // armor = 70 -> dmgReduction = 83.66600265340755%
        // armor = 80 -> dmgReduction = 89.44271909999159%
        // armor = 90 -> dmgReduction = 94.86832980505138%
        // armor = 99 -> dmgReduction = 99.498743710662%
        // armor = 100 -> dmgReduction = 100%

        float dmgReductionPercent = Mathf.Sqrt(armor * 100f);
        float reducedAmount = dmgReductionPercent * dmgAmount * 0.01f;
        return reducedAmount;
    }
    protected virtual void Death()
    {
        Debug.Log("Ded");
        gameObject.SetActive(false);
    }
}
