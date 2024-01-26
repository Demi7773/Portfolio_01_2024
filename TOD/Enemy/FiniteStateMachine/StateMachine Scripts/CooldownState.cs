using TOD.Statemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Cooldown", menuName = "StateMachine/Cooldown")]
public class CooldownState : State
{
    float timer;
    [SerializeField] float timeBetweenAttacks;
    //[SerializeField] State chaseState;
    //[SerializeField] State attackState;

    public override void Think(EnemyBehaviour enemy)
    {
        enemy.cooldownTimer += Time.deltaTime;
        if(enemy.cooldownTimer>=timeBetweenAttacks)
        {
            if (enemy.playerTransform == null)
            {
                PlayerEvents.NeedPlayerReference?.Invoke();
            }

            if (Vector3.Distance(enemy.transform.position,enemy.playerTransform.position) <= enemyStats.shootRange)
            {
                Debug.Log("Player in Range again, switching to AttackState");
                enemy.cooldownTimer = 0;
                //enemy.enemyState = attackState;
                enemy.GoToAttackState();
            }
            else
            {
                Debug.Log("Player out of range, switching to ChaseState");
                enemy.cooldownTimer = 0;
                //enemy.enemyState = chaseState;
                enemy.GoToChaseState();
            }
        }
        base.Think(enemy);
    }
}
