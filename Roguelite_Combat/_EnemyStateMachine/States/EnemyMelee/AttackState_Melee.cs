using UnityEngine;

public class AttackState_Melee : AttackState_Base
{
    [Header("Melee specific")]
    [SerializeField] protected GameObject attackCircleTemp;
    [SerializeField] protected float attackRange = 1.0f;
    [SerializeField] protected float attackDuration = 0.1f;

    //[Space(20)]
    //[Header("Debug")]
    //[SerializeField] protected List<Transform> hitTargets = new List<Transform>();



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        Debug.Log("Entering AttackState_Melee");
        attackCircleTemp.SetActive(true);
    }
    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting AttackState_Melee");
        attackCircleTemp.SetActive(false);
    }
    public override void Step()
    {
        base.Step();

        timer += Time.deltaTime;
        if (timer >= attackDuration)
        {
            enemyBehavior.SwitchToCooldownState();
        }
        AttackingBehavior();
    }

    protected virtual void AttackingBehavior()
    {
        //if (Vector3.Distance(attackPoint.position, enemyBehavior.PlayerPosition) <= enemyBehavior.AttackRange)
        if (Physics.CheckSphere(attackPoint.position, attackRange, enemyBehavior.TargetLayers))
        {
            PlayerHP playerHPScript = enemyBehavior.Player.GetComponent<PlayerHP>();
            if (playerHPScript != null)
            {
                playerHPScript.GetHitFor(enemyBehavior.Damage);
            }
            else
            {
                Debug.Log("PlayerHPScript null!");
            }
        }
    }

}
