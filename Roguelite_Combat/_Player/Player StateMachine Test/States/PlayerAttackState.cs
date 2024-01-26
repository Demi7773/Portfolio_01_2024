using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    [Header("Dependencies")]
    [Space(20)]
    [SerializeField] protected LayerMask enemiesLayer;
    [Header("Set in inspector")]
    [SerializeField] protected float radius = 0.5f;

    [SerializeField] protected float rotationArc = 90.0f;
    protected float rotationSpeed => -rotationArc / attackDmgDuration;

    [SerializeField] protected Quaternion startingRotation;
    [SerializeField] protected GameObject attackPointRotator;

    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected GameObject attackCircleTemp;
    [Space(20)]
    [Header("Timer")]
    [SerializeField] protected float timer = 0.0f;
    [SerializeField] protected float attackDuration = 0.4f;
    [SerializeField] protected float attackDmgDuration = 0.2f;
    [Space(30)]
    [Header("Debug")]
    [SerializeField] protected List<IDamageable> hitTargets = new List<IDamageable>();

    protected float damage => stats.Damage;
    protected float critChance => stats.CritChance;
    protected float critDamageMod => stats.CritDamageMod;
    


    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        timer = 0.0f;
        startingRotation = attackPointRotator.transform.rotation;
        attackCircleTemp.SetActive(true);
    }
    public override void ExitState()
    {
        base .ExitState();

        attackPointRotator.transform.rotation = startingRotation;
        attackCircleTemp.SetActive(false);
        hitTargets.Clear();
    }
    public override void Step()
    {
        base.Step();

        timer += Time.deltaTime;

        if (timer <= attackDmgDuration)
        {
            CheckTargetsHit();
            RotateAttackPoint();
        }
        else
        {
            attackPointRotator.transform.rotation = startingRotation;
            attackCircleTemp.SetActive(false);
        }

        

        if (timer >= attackDuration) 
        {
            playerStateMachine.SwitchToDefaultState();
        }
    }


    protected virtual void CheckTargetsHit()
    {
        RaycastHit[] hits = Physics.SphereCastAll(attackPoint.position, radius, Vector3.up, radius, enemiesLayer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.GetComponent<IDamageable>() != null)
            {
                IDamageable target = hit.transform.GetComponent<IDamageable>();
                if (hitTargets.Contains(target))
                {
                    Debug.Log("Target already on hitTargets, skipping");
                }
                else
                {
                    Debug.Log("Attack hit target: " + hit.transform.name);
                    DamageTarget(target);
                    hitTargets.Add(target);
                }
            }
            else
            {
                Debug.Log("hit does not have IDamageable, skipping");
            }
        }
    }

    protected virtual void RotateAttackPoint()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        attackPointRotator.transform.Rotate(0.0f, rotationStep, 0.0f);
        //Debug.Log("Rotation: " + attackPointRotator.transform.rotation.eulerAngles);
    }


    protected virtual void DamageTarget(IDamageable target)
    {
        float dmg = damage;

        float rollForCrit = Random.Range(0.0f, 99.0f);
        //Debug.Log("Roll for Crit: " + rollForCrit);

        if (rollForCrit <= critChance)
        {
            dmg = damage * critDamageMod;
            Debug.Log("Crit! Dmg: " + dmg);
        }

        target.GetHitFor(dmg);
    }
}
