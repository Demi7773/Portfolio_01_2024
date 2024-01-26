using TOD.Statemachine;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Patrol", menuName = "StateMachine/Patrol")]
public class PatrolState : State
{

    [SerializeField] float distanceFromWaypoint;
    [SerializeField] LayerMask playerLayer;
    //[SerializeField] State attackState;



    public override void Think(EnemyBehaviour enemy)
    {
        CheckForPlayer(enemy);
        ContinuePatrol(enemy);

        base.Think(enemy);
    }

    void CheckForPlayer(EnemyBehaviour enemy)
    {
        if (Physics.CheckSphere(enemy.transform.position, enemyStats.visionRange, playerLayer))
        {
            //enemy.enemyState = attackState;
            enemy.GoToAttackState();
        }
    }

    void ContinuePatrol(EnemyBehaviour enemy)
    {
        //Vector3 direction = (enemy.waypoints.point[enemy.currentWaypoint] - enemy.transform.position);
        //Quaternion rotationToWayPoint = Quaternion.LookRotation(direction);
        //enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotationToWayPoint, Time.deltaTime * enemyStats.enemyRotationSpeed);

        enemy.GetComponent<NavMeshAgent>().SetDestination(enemy.waypoints.point[enemy.currentWaypoint]);
        enemy.GetComponent<NavMeshAgent>().speed = enemyStats.patrolSpeed;

        if (Vector3.Distance(enemy.transform.position, enemy.waypoints.point[enemy.currentWaypoint]) < distanceFromWaypoint)
        {
            enemy.currentWaypoint++;
            Debug.Log(enemy.currentWaypoint);
        }
        if (enemy.currentWaypoint >= enemy.waypoints.point.Length)
        {
            enemy.currentWaypoint = 0;
        }
    }

}