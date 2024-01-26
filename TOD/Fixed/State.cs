using UnityEngine;
using TOD.Statemachine;

namespace TOD.Statemachine
{
    public abstract class State : ScriptableObject
    {
        [SerializeField] protected EnemyStats enemyStats;


        public virtual void Think(EnemyBehaviour enemy)
        {

        }

    }
}
