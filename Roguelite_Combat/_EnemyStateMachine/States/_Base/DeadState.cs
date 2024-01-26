using UnityEngine;
using UnityEngine.VFX;

public class DeadState : _EnemyState
{
    [SerializeField] protected GameObject body;
    [SerializeField] protected GameObject deadBody;
    [SerializeField] protected VisualEffect deathEffects;


    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        enemyBehavior.myCollider.enabled = false;
        body.SetActive(false);
        deadBody.SetActive(true);
        deathEffects.Play();
        Debug.Log("Entering DeadState");
    }
    public override void ExitState()
    {
        base.ExitState();

        enemyBehavior.myCollider.enabled = true;
        body.SetActive(true);
        deadBody.SetActive(false);
        Debug.Log("Exiting DeadState");
    }
    public override void Step()
    {
        
    }
}
