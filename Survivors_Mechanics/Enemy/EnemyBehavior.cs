using System.Collections;
using UnityEngine;
using static PlayerEvents;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] protected GameObject player;

    [Header("State Machine")]
    [SerializeField] protected PatrolState patrolState;
    [SerializeField] protected CombatState combatState;
    //[SerializeField] private PatrolPoints patrolPoints;
    [Space(10)]
    [SerializeField] protected State startingState;
    [Space(10)]
    [Header("Current State")]
    [SerializeField] protected State currentState;

    [Space(20)]
    [Header("Stats")]
    [Space(10)]
    [Header("General")]
    [SerializeField, Range(10.0f, 360.0f)] protected float rotateSpeed = 10.0f;
    [Space(10)]
    [Header("Patrol")]
    [SerializeField, Range(1.0f, 100.0f)] protected float patrolSpeed = 5.0f;
    [SerializeField, Range(0.0f, 10.0f)] protected float delayAtPatrolPoint = 1.0f;
    [Space(10)]
    [Header("Combat")]
    [SerializeField, Range(1.0f, 100.0f)] protected float combatSpeed = 10.0f;
    [SerializeField, Range(1.0f, 100.0f)] protected float damage = 10.0f;
    [SerializeField, Range(1.0f, 100.0f)] protected float attackRange = 10.0f;
    [SerializeField, Range(0.0f, 2.0f)] protected float delayBetweenCombatActions = 0.3f;
    [SerializeField, Range(0.0f, 10.0f)] protected float timeBetweenAttacks = 1.0f;


    public float RotateSpeed => rotateSpeed;
    public float PatrolSpeed => patrolSpeed;
    public float DelayAtPatrolPoint => delayAtPatrolPoint;
    public float CombatSpeed => combatSpeed;
    public float Damage => damage;
    public float AttackRange => attackRange;
    public float DelayBetweenCombatActions => delayBetweenCombatActions;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    




    protected virtual void OnEnable()
    {
        PlayerGO += PlayerRefrence;
        StartCoroutine(DelayBeforeInitialize(0.2f));
    }
    protected virtual void OnDisable()
    {
        PlayerGO -= PlayerRefrence;
    }
    private void PlayerRefrence(PlayerGOReference Player)
    {
        player = Player.playerGO;
    }



    protected IEnumerator DelayBeforeInitialize(float delay)
    {
        yield return new WaitForSeconds(delay);
        InitializeEnemy();
    }
    protected virtual void InitializeEnemy()
    {
        if (player == null)
        {
            NeedPlayerReference?.Invoke();
        }
        SwitchToStartingState();
    }



    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.Step();
        }
        else
        {
            Debug.Log("Current State is null");
        }
    }



    public virtual void SwitchToStartingState()
    {
        Debug.Log("Switching to Starting State");
        currentState = startingState;
        currentState.EnterState(this, player);
    }
    public virtual void SwitchToPatrolState()
    {
        if (player == null)
        {
            NeedPlayerReference?.Invoke();
        }
        Debug.Log("Switching to Patrol State");
        currentState = patrolState;
        currentState.EnterState(this, player);
    }
    public virtual void SwitchToCombatState()
    {
        if (player == null)
        {
            NeedPlayerReference?.Invoke();
        }
        Debug.Log("Switching to Combat State");
        currentState = combatState;
        currentState.EnterState(this, player);
    }


    public virtual void DeathBehavior()
    {
        currentState.ExitState();
        this.gameObject.SetActive(false);
    }
}
