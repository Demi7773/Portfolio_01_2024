using UnityEngine;
using static PlayerEvents;

public class EnemyStupid : MonoBehaviour
{
    [SerializeField] protected bool isPaused = false;

    [SerializeField] protected GameObject player;
    [SerializeField] protected PlayerHP playerHPScript;
    //[SerializeField] protected PlayerXP playerXPScript;
    [SerializeField] protected EnemySpawner enemySpawner;
    [SerializeField] protected LevelManager levelManager;
    [SerializeField] protected int xpValue = 1;

    [Space(20)]
    [Header("Stats")]
    [SerializeField, Range(0f, 1000f)] protected float currentHP;
    [SerializeField, Range(1f, 1000f)] protected float maxHP;
    [SerializeField, Range(0.0f, 99.0f)] protected float armor;
    [Space(10)]
    [SerializeField, Range(1.0f, 100.0f)] protected float damage = 10.0f;
    [SerializeField, Range(1.0f, 100.0f)] protected float attackRange = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] protected float timeBetweenAttacks = 1.0f;
    [Space(10)]
    [SerializeField, Range(1.0f, 360.0f)] protected float rotateSpeed = 10.0f;
    [SerializeField, Range(1.0f, 100.0f)] protected float moveSpeed = 10.0f;



    protected void OnEnable()
    {
        currentHP = maxHP;
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
    }
    protected void OnDisable()
    {
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
    }
    protected void PauseMe()
    {
        isPaused = true;
    }
    protected void UnPauseMe()
    {
        isPaused = false;
    }



    public void InitializeEnemy(EnemySpawner spawner, GameObject playerRef, LevelManager lvlManager)
    {
        enemySpawner = spawner;
        player = playerRef;
        levelManager = lvlManager;

        if (player.GetComponent<PlayerHP>() != null)
        {
            playerHPScript = player.GetComponent<PlayerHP>();
        }
        else
        {
            Debug.Log("PlayerHP null");
        }

        //if (player.GetComponent<PlayerXP>() != null)
        //{
        //    playerXPScript = player.GetComponent<PlayerXP>();
        //}
        //else
        //{
        //    Debug.Log("PlayerXP null");
        //}
    }



    protected void Update()
    {
        if (!isPaused)
        {
            //FaceTowardsPlayer();
            MoveInPlayerDirection();
            if (IsPlayerInAttackRange())
            {
                AttackPlayer();
            }
        }
    }

    //protected void FaceTowardsPlayer()
    //{
    //    transform.Rotate(TowardsPlayer());
    //}
    protected void MoveInPlayerDirection()
    {
        transform.position += TowardsPlayer() * moveSpeed * Time.deltaTime;
    }
    protected Vector3 TowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        return direction;
    }



    protected void AttackPlayer()
    {
        playerHPScript.GetHitFor(damage);
    }
    protected bool IsPlayerInAttackRange()
    {
        if (DistanceToPlayer() > attackRange)
            return false;

        else
            return true;
    }
    protected float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }



    public void TakeDamage(float dmgAmount)
    {
        float dmgAfterAdjustment = dmgAmount - ArmorDamageReduction(dmgAmount, armor);
        LoseHP(dmgAfterAdjustment);
    }
    protected float ArmorDamageReduction(float dmgAmount, float armorForCalculation)
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

        float dmgReductionPercent = Mathf.Sqrt(armorForCalculation * 100f);
        float reducedAmount = dmgReductionPercent * dmgAmount * 0.01f;
        return reducedAmount;
    }
    protected void LoseHP(float amount)
    {
        float newHP = currentHP - amount;
        if (newHP <= 0f)
        {
            currentHP = 0f;     
            Death();
        }
        else
        {
            currentHP = newHP;
        }
    }
    protected void Death()
    {
        DropExpPickup();
        //playerXPScript.GetXP(xpValue);
        enemySpawner.ReturnEnemyToPool(this.gameObject);
    }

    private void DropExpPickup()
    {
        GameObject expDrop = levelManager.GetEXPPickupFromPool();
        expDrop.GetComponent<IEXP>().SetMyValue(xpValue);
        expDrop.transform.position = transform.position;

        expDrop.SetActive(true);
    }
}
