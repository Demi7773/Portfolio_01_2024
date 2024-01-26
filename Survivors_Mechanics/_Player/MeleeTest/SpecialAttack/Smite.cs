using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class Smite : SpecialAttack
{
    [Header("VFX settings")]
    [SerializeField] protected GameObject specialAttackVFXPrefab;
    [SerializeField, Range(0.1f, 10.0f)] protected float vfxDuration = 1.0f;

    [SerializeField] protected Transform specialAttackVFXParent;
    [SerializeField] protected Queue<GameObject> specialAttackVFXQueue = new Queue<GameObject>();

    [Header("Base values")]
    [SerializeField, Range(1.0f, 100.0f)] protected float baseDamage = 20.0f;
    [SerializeField, Range(1.0f, 20.0f)] protected float range = 5.0f;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected int maxTargets = 10;

    [Header("After Modification from PlayerStats")]
    [SerializeField] protected float damage;





    protected override void UpdateStatsFromPlayerStats()
    {
        condition = playerStats.EquippedSpecialAttackCondition;
        damage = baseDamage * playerStats.SpecialAttackDamageMultiplier;
    }



    // Add UI to Condition and this
    // Myb change parent class and Attack to Scriptables / static?
    protected override void InitializeMe()
    {
        //base.InitializeMe();

        for (int i = 0; i < maxTargets * 3; i++)
        {
            GameObject specialAttackVFX = Instantiate(specialAttackVFXPrefab, specialAttackVFXParent);

            if (specialAttackVFX.GetComponent<SpecialAttackVFXController>() != null)
            {
                specialAttackVFX.GetComponent<SpecialAttackVFXController>().InitializeMe(this, vfxDuration);
            }
            else
            {
                Debug.LogError("SpecialAttackVFX Script is null");
            }

            specialAttackVFX.SetActive(false);
        }
    }


        // Pool
    public virtual void AddSpecialEffectToQueue(GameObject specialAttackVFX)
    {
        specialAttackVFXQueue.Enqueue(specialAttackVFX);
        //StartCoroutine(SmallDelayBeforeAddToQueue(specialAttackVFX));
    }
    protected void ExpandPoolIfRunningEmpty()
    {
        if (specialAttackVFXQueue.Count < maxTargets)
        {
            InitializeMe();
        }
    }


        // Use, Cancel and Effect
    public override void UseSpecialAttack()
    {
        base.UseSpecialAttack();
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if (hit.GetComponent<EnemyStupid>() != null)
                {
                    PlayerSpecialAttackTick?.Invoke();
                    SpecialAttackHitsEnemy(hit.GetComponent<EnemyStupid>());
                }
                else
                {
                    Debug.Log("Enemy script is null!");
                }
            }
        }
        else
        {
            Debug.Log("Smite has no available target, cancelling");
            condition.CancelSpecialAttackUse();
        }
    }
    protected virtual void SpecialAttackHitsEnemy(EnemyStupid enemyScript)
    {
        Vector3 enemyPos = enemyScript.transform.position;
        enemyScript.TakeDamage(damage);
        GameObject specialVFX = specialAttackVFXQueue.Dequeue();
        ExpandPoolIfRunningEmpty();
        specialVFX.transform.position = enemyPos;
        specialVFX.SetActive(true);
    }
}
