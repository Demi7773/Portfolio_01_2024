using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpScript : HPScriptBase
{
    [SerializeField] private float baseMaxHP = 100f;


    public float CurrentHPRatio() { return hpCurrent / hpMax; }

    public virtual void Awake()
    {
        SetMaxHP(baseMaxHP);
    }

    public virtual void SetMaxHP(float newMaxHP)
    {
        hpMax = newMaxHP;
        Debug.Log("Enemy max hp set to " +  hpMax);
        HealToFull();
    }
    protected virtual void OnEnable()
    {
        HealToFull();
    }



    // add FX
    public override void LoseHP(float dmgAmount)
    {
        hpCurrent -= dmgAmount;
        hpCurrent = Mathf.Clamp(hpCurrent, 0f, hpMax);
        if (hpCurrent <= 0f)
        {
            Death();
        }
    }
    public override void HealHP(float healAmount)
    {
        hpCurrent += healAmount;
        hpCurrent = Mathf.Clamp(hpCurrent, 0f, hpMax);
    }
    protected override void Death()
    {
        Debug.Log("Enemy Death");
        gameObject.SetActive(false);
    }
}
