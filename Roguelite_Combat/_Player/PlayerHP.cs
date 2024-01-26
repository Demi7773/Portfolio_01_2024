using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private UIManager uiManager;
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private float currentHP = 50.0f;
    [Space(20)]
    [Header("State Machine")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [Space(20)]
    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTimer = 0.0f;
    [SerializeField] private float invulnerabilityOnDamageDuration = 0.2f;
    //[SerializeField] private float invulnerabilityOnDodgeDuration = 0.3f;

    [SerializeField] private float maxHP => stats.MaxHP;
    [SerializeField] private float armor => stats.Armor;



    public bool IsDamageable()
    {
        if (invulnerabilityTimer >= 0.0f)
        {
            return false;
        }
        return true;
    }
    public float HPRatio()
    {
        return currentHP / maxHP;
    }


    private void Start()
    {
        currentHP = stats.MaxHP;
        uiManager.UpdateHPUI();
    }



    private void OnEnable()
    {
        invulnerabilityTimer = 0.0f;
    }
    


        // invulnerability timer
    private void Update()
    {
        invulnerabilityTimer -= Time.deltaTime;
    }



        // Main SetHP
    private void SetNewHPValue(float newValue)
    {
        currentHP = Mathf.Clamp(newValue, 0.0f, maxHP);
        uiManager.UpdateHPUI();
    }

        // healing
    public bool IsHealable()
    {
        if (currentHP < maxHP)
        { return true; }
        else
        { return false; }
    }
    public void HealFor(float healAmount)
    {
        float newHP = currentHP + healAmount;
        SetNewHPValue(newHP);
    }
    public void HealToFull()
    {
        HealFor(1000.0f);
    }

        // GetHit, DamageReduction, Invulnerability, Death
    public virtual void GetHitFor(float dmgAmount)
    {
        if (IsDamageable())
        {
            invulnerabilityTimer = invulnerabilityOnDamageDuration;

            float newHP = currentHP - DamageReducedByDmgReduction(dmgAmount);
            SetNewHPValue(newHP);            

            if (currentHP <= 0.0f)
            {
                Death();
            }
            else
            {
                stateMachine.SwitchToStaggerState();
                //playerModel.DOShakeScale(shakeDuration, shakeStrength);
                //StartCoroutine(Invulnerability(invulnerabilityOnDamageDuration));
            }
        }

        else
        {
            Debug.Log("Player is Invulnerable");
        }
    }
    private float DamageReducedByDmgReduction(float originalDmgAmount)
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
        float reducedAmount = dmgReductionPercent * originalDmgAmount * 0.01f;
        return reducedAmount;
    }
    public void DodgeRollInvulnerability(float duration)
    {
        invulnerabilityTimer = duration;
    }
    private void Death()
    {
        Debug.Log("Player Ded, Game Over");
        //gameObject.SetActive(false);
    }

}
