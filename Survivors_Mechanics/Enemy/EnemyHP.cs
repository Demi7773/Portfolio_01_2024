using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable, IEnemy
{
    [SerializeField, Range (0f, 1000f)] private float currentHP;
    [SerializeField, Range (1f, 1000f)] private float maxHP;
    [SerializeField, Range (0f, 99.9f)] private float armor;

    [SerializeField] protected EnemyBehavior enemyBehavior;



    // Add pooling later
    private void OnEnable()
    {
        currentHP = maxHP;
    }
    private void OnDisable()
    {
        
    }



    public void GetHitFor(float amount)
    {
        LoseHP(amount);
    }
    private void LoseHP(float amount)
    {
        amount = ReduceDamageAmountByArmor(amount);
        float newHP = currentHP - amount;
        Debug.Log("Enemy was hit for " + amount + "dmg, new HP " + newHP);
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
    private float ReduceDamageAmountByArmor(float amount)
    {
        float newAmount = amount * (1 - armor * 0.01f);
        return newAmount;
    }

    private void Death()
    {
        Debug.Log("Enemy died, sending to EnemyBehavior");
        //enemyBehavior.DeathBehavior();
    }
}
