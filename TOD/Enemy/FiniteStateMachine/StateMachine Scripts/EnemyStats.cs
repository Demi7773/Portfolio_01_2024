using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TOD.Statemachine
{
 

    [CreateAssetMenu(fileName = "Enemy Stats", menuName = "StateMachine/Enemy Stats")]
    public class EnemyStats : ScriptableObject
    {
        [Space(10)]
        public int shootRange;
        public float patrolSpeed;
        public float chaseSpeed;
        [Space(10)]
        public float enemyRotationSpeed;
        [Space(10)]
        public float visionRange;
        public float shootCooldown;
        [Space(10)]
        public float enemyMaxHP;
        public float dmgReduction;
        [Space(10)]
        public Rigidbody enemyRigidbody;
        public NavMeshAgent enemyNavMeshAgent;

    }
}