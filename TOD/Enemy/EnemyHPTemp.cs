using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHPTemp : MonoBehaviour
{
    [Header("PlugIns")]
    //[SerializeField] EquipmentController equipmentControls;

    [Header("HP Stats")]
    [SerializeField] private float enemyCurrentHP;
    [SerializeField] private float enemyMaxHP;
    private float enemyMaxHPDefault = 100f;

    [Header("HP Stats")]
    [SerializeField] private float dmgReduction;
    private float dmgReductionDefault = 1f;



    private void OnEnable()
    {
        SetNewHPFromEquipment();
        SetNewDmgReductionFromEquipment();
    }
    private void OnDisable()
    {
        // add pooling mechanics if necessary
    }

    private void Start()
    {
        SetNewHPFromEquipment();
        SetNewDmgReductionFromEquipment();
    }

    // Public methods to update values for new equipment modifiers
    // Needs to be called somewhere in Equipment/UI
    public void SetNewHPFromEquipment()
    {
        // add UI
        enemyMaxHP = enemyMaxHPDefault /** equipmentControls.PlayerHPStat*/;
        SetToFullHP();
    }
    public void SetNewDmgReductionFromEquipment()
    {
        dmgReduction = dmgReductionDefault /** equipmentControls.PlayerDmgReducStat*/;
    }


    // HealToFull and Heal
    public void SetToFullHP()
    {
        // add UI?
        enemyCurrentHP = enemyMaxHP;
    }
    public void HealHP(float healAmount)
    {
        // add UI
        enemyCurrentHP += healAmount;
        if (enemyCurrentHP > enemyMaxHP)
        {
            enemyCurrentHP = enemyMaxHP;
        }
    }


    // LoseHP with DamageReduction modifier
    public void LoseHP(float dmg)
    {
        Debug.Log("Dmg to Enemy in " + dmg);
        dmg -= dmg * (dmgReduction / 100f);
        Debug.Log("Dmg to Enemy reduced to " + dmg);
        enemyCurrentHP -= dmg;
        if (enemyCurrentHP <= 0f)
        {
            Death();
        }
        // add UI
    }

    // LoseHP with DamageReduction modifier and IFrames
    //public void LoseHP(float dmg)
    //{
    //    add more if necessary
    //    if (!isInvulnerable)
    //        {
    //            Debug.Log("Dmg in " + dmg);
    //            dmg -= dmg * dmgReduction;
    //            Debug.Log("Dmg reduced to " + dmg);
    //            playerCurrentHP -= dmg;
    //            if (playerCurrentHP <= 0f)
    //            {
    //                // add GameOver
    //                Debug.Log("Game Over");
    //            }
    //            Debug.Log("PlayerHP " + playerCurrentHP + "/" + playerMaxHP);
    //            // add UI
    //            StartCoroutine("Invulnerable");
    //        }
    //        else
    //            Debug.Log("isInvulnerable, no dmg taken");
    //    // add more if necessary
    //}



    // PERCENT HP GAIN AND LOSS - calculates flat values and calls HealHP/LoseHP
    //IEnumerator Invulnerable()
    //{
    //    isInvulnerable = true;
    //    yield return new WaitForSeconds(invulnerabilityDuration);
    //    isInvulnerable = false;
    //}




    public void LoseHPPercentMax(float percentLoss)
    {
        float dmg = percentLoss * 0.01f * enemyMaxHP;
        LoseHP(dmg);
    }
    public void LoseHPPercentCurrent(float percentLoss)
    {
        float dmg = percentLoss * 0.01f * enemyMaxHP;
        LoseHP(dmg);
    }
    public void HealHPPercentMax(float percentHeal)
    {
        float healAmount = percentHeal * 0.01f * enemyMaxHP;
    }
    //public void HealHPPercentCurrent(float percentHeal)
    //{
    //    float healAmount = percentHeal * 0.01f * playerCurrentHP;
    //}


    private void Death()
    {
        Debug.Log("Enemy ded");
        this.gameObject.SetActive(false);
    }
}
