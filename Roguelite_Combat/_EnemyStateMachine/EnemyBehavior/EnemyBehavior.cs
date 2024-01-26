using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    [Header("Player")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected LayerMask targetLayers;
    [SerializeField] protected Transform currentTarget;
    [Space(20)]

    [Header("Modules")]
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected EnemyMovementModule movementDecisionsModule;
    [SerializeField] protected AimModule aimModule;
    [SerializeField] protected SelfDestruct selfDestruct;
    [Space(20)]

    [Header("Stats")]
    [SerializeField] public Collider myCollider;
    [SerializeField] protected bool isDamageable = true;
    [SerializeField] protected float currentHP = 30.0f;
    [Space(20)]

    [Header("StateMachine")]
    [SerializeField] protected _EnemyState currentState;
    [SerializeField] protected _EnemyState defaultState;
    [SerializeField] protected IdleState_Base idle;
    [SerializeField] protected CombatState_Base combat;
    [SerializeField] protected AttackState_Base attack;
    [SerializeField] protected CooldownState cooldown;
    [SerializeField] protected StaggerState stagger;
    [SerializeField] protected DeadState deadState;

    [Space(20)]
    [Header("Visuals")]
    [SerializeField] protected ShakeOnDamage shakeEffect;
    [SerializeField] protected FloatingHPBar hpBar;

    



        // public readonly
    public GameObject Player => player;
    public Vector3 PlayerPosition => player.transform.position;
    public LayerMask TargetLayers => targetLayers;

    public Transform CurrentTarget => currentTarget;
    //public Vector3 CurrentTargetPosition => currentTarget.position;
    public float DistanceToTarget => Vector3.Distance(transform.position, currentTarget.position);


    public EnemyStats Stats => stats;
    public EnemyMovementModule Movement => movementDecisionsModule;
    public Vector3 ChosenMoveDirection => movementDecisionsModule.ChosenDirectionTransform.forward;
    //public ObstacleDetectionModule ObstacleDetection => obstacleDetectionModule;
    public AimModule Aim => aimModule;


    //public Collider MyCollider => myCollider;


    public float CurrentHP => currentHP;
    public float MaxHP => stats.MaxHP   /*maxHP*/;
    public float Armor => stats.Armor   /*armor*/;
    public float RotationSpeed => stats.RotationSpeed;
    public float DetectionRadius => stats.DetectionRadius   /*detectionRadius*/;
    public float IdleSpeed => stats.IdleSpeed;
    public float CombatSpeed => stats.CombatSpeed       /*combatSpeed*/;
    public float TimeBetweenAttacks => stats.TimeBetweenAttacks     /*timeBetweenAttacks*/;
    public float AttemptAttackRange => stats.AttemptAttackRange     /*attemptAttackRange*/;
    public float Damage => stats.Damage     /*damage*/;
    public float AttackCooldownDuration => stats.AttackCooldownDuration     /*attackCooldownDuration*/;
    public float BaseStaggerDuration => stats.BaseStaggerDuration       /*baseStaggerDuration*/;


    public IdleState_Base Idle => idle;
    public CombatState_Base Combat => combat;
    public AttackState_Base Attack => attack;
    public CooldownState Cooldown => cooldown;
    public StaggerState Stagger => stagger;


    public ShakeOnDamage ShakeEffect => shakeEffect;







    public void SetNewTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }




        // public calls for State change
    public virtual void SwitchToDefaultState()
    {
        currentState.ExitState();
        currentState = defaultState;
        currentState.EnterState(this);
    }

    public virtual void SwitchToIdleState()
    {
        currentState.ExitState();
        currentState = idle;
        currentState.EnterState(this);
    }
    public virtual void SwitchToCombatState()
    {
        currentState.ExitState();
        currentState = combat;
        currentState.EnterState(this);
    }
    public virtual void SwitchToAttackState()
    {
        currentState.ExitState();
        currentState = attack;
        currentState.EnterState(this);
    }
    public virtual void SwitchToCooldownState()
    {
        currentState.ExitState();
        currentState = cooldown;
        currentState.EnterState(this);
    }
    public virtual void SwitchToStaggerState()
    {
        currentState.ExitState();
        currentState = stagger;
        currentState.EnterState(this);
    }
    public virtual void SwitchToDeadState()
    {
        currentState.ExitState();
        currentState = deadState;
        currentState.EnterState(this);
    }



    // Behavior
    protected virtual void OnEnable()
    {
        myCollider = GetComponent<Collider>();
        if (myCollider == null)
        {
            Debug.Log("Enemy collider null!");
            myCollider = gameObject.AddComponent<Collider>();
        }
        myCollider.enabled = true;

        currentHP = MaxHP;
        isDamageable = true;
    }
    public virtual void SetPlayerReference(GameObject playerRef)
    {
        player = playerRef;
    }
    protected virtual void Start()
    {
        currentState = defaultState;
        currentState.EnterState(this);
    }
    protected virtual void Update()
    {
        currentState.Step();
    }



        // HP and Death
    public float HPRatio()
    {
        return currentHP / MaxHP;
    }
    public virtual void GetHitFor(float dmgAmount)
    {
        if (isDamageable)
        {
            float reducedDmgAmount = ApplyDamageReduction(dmgAmount);
            float newHP = currentHP - reducedDmgAmount;


            currentHP = Mathf.Clamp(newHP, 0.0f, MaxHP);
            hpBar.HPBarUpdate(HPRatio());

            if (currentHP > 0.0f)
            {
                SwitchToStaggerState();
            }
            else
            {
                Death();
            }
        }

        else
        {
            Debug.Log("Enemy isnt damageable");
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

        float dmgReductionPercent = Mathf.Sqrt(Armor * 100f);
        float reducedAmount = dmgReductionPercent * dmgAmount * 0.01f;
        return reducedAmount;
    }
    protected virtual void Death()
    {
        myCollider.enabled = false;
        isDamageable = false;
        //DeathVisuals();

        if (selfDestruct != null)
        {
            selfDestruct.Explode(transform);
        }

        SwitchToDeadState();

        Debug.Log("Ded");
    }


        // Visual Effects and UI
    protected virtual void DeathVisuals()
    {

    }
}