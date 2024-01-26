using System.Collections;
using UnityEngine;
using static PlayerEvents;

public class Attack : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] protected PlayerStats playerStats;

    [SerializeField] protected GameObject attackSlashPrefab;
    [SerializeField] protected SlashBehaviour slashBehaviour;
    protected GameObject attackSlashInstance;

    [SerializeField] protected Transform attackParentObject;
    [SerializeField] protected Transform attackTransform;

    [Space(20)]
    [Header("Stats")]
    [SerializeField, Range(1.0f, 100.0f)] protected float attackDamageBase = 10.0f;
    //[SerializeField, Range(0.1f, 1.0f)] protected float attackLockActionsDuration = 0.1f;
    //[SerializeField, Range(0.1f, 3.0f)] protected float attackInstanceDuration = 0.3f;
    [SerializeField, Range(0.01f, 100.0f)] protected float attackCooldownDurationBase = 0.9f;
    //[SerializeField, Range(0.1f, 100.0f)] protected float attackSlashForwardSpeed = 1.0f;

    [Header("Stats after PlayerStats Modification")]
    [SerializeField] protected float attackDamageTotal;
    [SerializeField] protected float attackLockActionsDuration;
    [SerializeField] protected float attackInstanceDuration;
    [SerializeField] protected float attackCooldownDurationTotal;
    [SerializeField] protected float attackSlashForwardSpeed;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] protected bool isAttacking = false;
    public bool IsAttacking => isAttacking;


    [SerializeField] protected bool isAttackOnCooldown = false;
    public bool IsAttackOnCooldown => isAttackOnCooldown;



    public void SetPlayerStatsReference(PlayerStats stats)
    {
        playerStats = stats;
    }

    protected void OnEnable()
    {
        PlayerStatsChange += UpdateStatsFromPlayerStats;

        InitializeAttack();
    }
    protected void OnDisable()
    {
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
        StopAllCoroutines();

        isAttacking = false;
        isAttackOnCooldown = false;
    }

    protected void UpdateStatsFromPlayerStats()
    {
        attackDamageTotal = attackDamageBase * playerStats.AttackDamageMultiplier;
        attackLockActionsDuration = playerStats.AttackLockActionsDuration;
        attackInstanceDuration = playerStats.AttackInstanceDuration;
        attackCooldownDurationTotal = attackCooldownDurationBase * playerStats.AttackCooldownDurationMultiplier;
        attackSlashForwardSpeed += playerStats.AttackSlashForwardSpeed;
    }

    private void InitializeAttack()
    {
        isAttacking = false;
        isAttackOnCooldown = false;
        if (attackSlashInstance == null)
        {
            attackSlashInstance = Instantiate(attackSlashPrefab, attackParentObject);
            Debug.Log("attackSlashInstance is null, instantiating new");
        }
        SetInstanceScriptReference();
        attackSlashInstance.SetActive(false);
    }
    protected void SetInstanceScriptReference()
    {
        if (attackSlashInstance.GetComponent<SlashBehaviour>() != null)
            slashBehaviour = attackSlashInstance.GetComponent<SlashBehaviour>();
        else
            Debug.LogError("SlashBehaviour on attackSlashInstance is null!");
    }




    //public void TryAttack()
    //{
    //    if (!isAttackOnCooldown)
    //    {
    //        CreateAttack();
    //    }
    //}
    public void CreateAttack()
    {
        StartCoroutine(AttackLockOtherActionsTimer(attackLockActionsDuration));
        StartCoroutine(AttackCooldown(attackCooldownDurationTotal));

        slashBehaviour.SetMyStats(attackDamageTotal, attackInstanceDuration, attackSlashForwardSpeed);

        attackSlashInstance.transform.position = attackTransform.position;
        attackSlashInstance.transform.rotation = attackTransform.rotation;

        attackSlashInstance.SetActive(true);
    }



    protected IEnumerator AttackLockOtherActionsTimer(float duration)
    {
        isAttacking = true;
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }
    protected IEnumerator AttackCooldown(float duration)
    {
        isAttackOnCooldown = true;
        yield return new WaitForSeconds(duration);
        isAttackOnCooldown = false;
    }
}
