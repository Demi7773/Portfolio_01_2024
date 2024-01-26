using System.Collections;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour
{
    [Header("PlugIns")]
    [SerializeField] EquipmentController equipmentControls;
    [SerializeField] PlayerMovement movement;

    [Header("HP Stats")]
    [SerializeField] private float playerCurrentHP;
    [SerializeField] private float playerMaxHP;
    private float playerMaxHPDefault = 100f;

    public float GetPlayerCurrentHP => playerCurrentHP;
    public float GetPlayerMaxHP => playerMaxHP;

    [Header("HP Stats")]
    [SerializeField] private float dmgReduction;
    private float dmgReductionDefault = 1f;

    [Header("IFrames on LoseHP")]
    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private bool isInvulnerable = false;


    public EquipmentController Equipment => equipmentControls;


    // DEVBUILD TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoseHP(5000f);
        }
    }

    private float HPRatioForUI()
    {
        float temp;
        temp= playerCurrentHP / playerMaxHP;
        return temp;
    }



    private void OnEnable()
    {
        movement.enabled = true;
    }


    private void Awake()
    {
        equipmentControls.CalculateModifiers();
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
        playerMaxHP = playerMaxHPDefault + equipmentControls.PlayerHPStat;
        SetToFullHP();
        HUDEvents.PlayerHealthUpdateEvent?.Invoke(new PlayerHealthUpdateEventData(HPRatioForUI()));
    }
    public void SetNewDmgReductionFromEquipment()
    {
        dmgReduction = dmgReductionDefault + equipmentControls.PlayerDmgReducStat;
    }


    // HealToFull and Heal
    public void SetToFullHP() //ovo bi trebalo bit pozvano u startu, kaj ne?
    {
        playerCurrentHP = playerMaxHP;
        HUDEvents.PlayerHealthUpdateEvent?.Invoke(new PlayerHealthUpdateEventData(HPRatioForUI()));
    }

    public void HealHP(float healAmount) //odakle dolazi ovaj healAmount i kako?
    {
        playerCurrentHP += healAmount;
        if (playerCurrentHP > playerMaxHP)
        {
            playerCurrentHP = playerMaxHP;
        }
        HUDEvents.PlayerHealthUpdateEvent?.Invoke(new PlayerHealthUpdateEventData(HPRatioForUI()));
    }


    // LoseHP with DamageReduction modifier and IFrames
    public void LoseHP(float dmg)
    {
        if (!isInvulnerable)
        {
            AudioEvents.PlayShipDamagedSoundsEvent?.Invoke();
            Debug.Log("Dmg in " + dmg);
            dmg -= dmg * (dmgReduction / 100f);
            Debug.Log("Dmg reduced to " + dmg);
            playerCurrentHP -= dmg;

            if (playerCurrentHP <= 0f)
            {
                PlayerDeath();
            }
            //Debug.Log("PlayerHP " + playerCurrentHP + "/" + playerMaxHP);
            HUDEvents.PlayerHealthUpdateEvent?.Invoke(new PlayerHealthUpdateEventData(HPRatioForUI()));
            StartCoroutine("Invulnerable");
        }
        else
            Debug.Log("isInvulnerable, no dmg taken");
    }
    IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }



    // PERCENT HP GAIN AND LOSS - calculates flat values and calls HealHP/LoseHP
    public void LoseHPPercentMax(float percentLoss)
    {
        float dmg = percentLoss * 0.01f * playerMaxHP;
        LoseHP(dmg);
    }
    public void LoseHPPercentCurrent(float percentLoss)
    {
        float dmg = percentLoss * 0.01f * playerCurrentHP;
        LoseHP(dmg);
    }
    public void HealHPPercentMax(float percentHeal)
    {
        float healAmount = percentHeal * 0.01f * playerMaxHP;
        HealHP(healAmount);
    }
    //public void HealHPPercentCurrent(float percentHeal)
    //{
    //    float healAmount = percentHeal * 0.01f * playerCurrentHP;
    //}


    private void PlayerDeath()
    {
        RaisePanelsFromLevelsEvents.RaiseDefeatPanelEvent?.Invoke();
        PlayerEvents.GameOver?.Invoke();

        movement.enabled = false;
        Debug.Log("Game Over");
        // iskljuciti movement i shooting skriptu, enemyje vratit u patrol
    }
}
