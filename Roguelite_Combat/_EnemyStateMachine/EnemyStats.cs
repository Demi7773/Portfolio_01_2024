using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("Basic stats for EnemyBehavior")]
    //[SerializeField] protected float currentHP = 30.0f;
    [SerializeField] protected float maxHP = 30.0f;
    [SerializeField] protected float armor = 10.0f;
    [Space(10)]
    [SerializeField] protected float rotationSpeed = 10.0f;
    [Header("Idle")]
    [SerializeField] protected float detectionRadius = 5.0f;
    [SerializeField] protected float idleSpeed = 5.0f;
    [Space(10)]
    [Header("Combat")]
    [SerializeField] protected float combatSpeed = 4.0f;
    [SerializeField] protected float attemptAttackRange = 1.5f;
    [SerializeField] protected float timeBetweenAttacks = 2.0f;
    [Space(10)]
    [Header("Attack")]
    [SerializeField] protected float damage = 10.0f;
    [Space(10)]
    [Header("Cooldown")]
    [SerializeField] protected float attackCooldownDuration = 0.5f;
    [Space(10)]
    [Header("Stagger")]
    [SerializeField] protected float baseStaggerDuration = 0.3f;


    //public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public float Armor => armor;

    public float RotationSpeed => rotationSpeed;

    public float DetectionRadius => detectionRadius;
    public float IdleSpeed => idleSpeed;

    public float CombatSpeed => combatSpeed;
    public float AttemptAttackRange => attemptAttackRange;
    public float TimeBetweenAttacks => timeBetweenAttacks;

    public float Damage => damage;

    public float AttackCooldownDuration => attackCooldownDuration;

    public float BaseStaggerDuration => baseStaggerDuration;


}
