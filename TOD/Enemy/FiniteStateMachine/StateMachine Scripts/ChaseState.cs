using TOD.Statemachine;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Chase", menuName = "StateMachine/Chase")]
public class ChaseState : State
{
    //[SerializeField] State attackState;

    public override void Think(EnemyBehaviour enemy)
    {
        if (enemy.playerTransform != null)
        {
            Transform player = enemy.playerTransform;
            Transform enemyTransform = enemy.transform;


            Vector3 direction = (player.position - enemyTransform.position).normalized;
            Quaternion rotationToWaypoint = Quaternion.LookRotation(direction);


            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rotationToWaypoint, Time.deltaTime *
                /*2*/enemy.enemyStats.enemyRotationSpeed);
            // *************************************OVO GORE JA ZAMJENIO SA ROTATIONSPEED*************************************


            // enemy.GetComponent<Rigidbody>().velocity = enemy.transform.forward * Time.deltaTime * enemyStats.chaseSpeed;
            enemy.GetComponent<NavMeshAgent>().SetDestination(player.position);
            enemy.GetComponent<NavMeshAgent>().speed = enemyStats.chaseSpeed;


            if (Vector3.Distance(player.position, enemyTransform.position) <= enemyStats.shootRange)
            {
                Debug.Log("Player in Range again, switching to AttackState");
                //enemy.enemyState = attackState;
                enemy.GoToAttackState();
            }
        }

        else
        {
            PlayerEvents.NeedPlayerReference?.Invoke();
        }        

        base.Think(enemy);
    }
}
