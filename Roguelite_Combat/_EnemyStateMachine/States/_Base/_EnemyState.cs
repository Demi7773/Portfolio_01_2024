using UnityEngine;

public class _EnemyState : MonoBehaviour
{
    [SerializeField] protected EnemyBehavior enemyBehavior;
    public virtual void EnterState(EnemyBehavior enemy)
    {
        enemyBehavior = enemy;
    }
    public virtual void Step()
    {

    }
    public virtual void ExitState() 
    {

    }
}
