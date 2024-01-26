using UnityEngine;

public class AttackState_Turret : AttackState_Base
{
    [Header("Turret Specific")]
    [SerializeField] protected EnemyBehavior_Turret enemyBehaviorTurret;
    [Space(10)]
    [SerializeField] protected int shotsFired = 0;
    [SerializeField] protected int numberOfShots = 3;
    [Space(10)]
    [SerializeField] protected float shotSequenceTimer = 0.0f;
    [SerializeField] protected float timeBetweenShotsInSequence = 0.3f;
    [Space(10)]
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float aimOffset = 1.0f;



    public override void EnterState(EnemyBehavior enemy)
    {
        base.EnterState(enemy);

        if (enemy is EnemyBehavior_Turret)
        {
            enemyBehaviorTurret = enemy as EnemyBehavior_Turret;
            shotsFired = 0;
            shotSequenceTimer = 0.0f;

            //Debug.Log("Entering AttackState_Turret");
        }
        else
        {
            Debug.Log("Attached EnemyBehavior is not EnemyBehavior_Turret!");
        }
    }
    public override void ExitState()
    {
        base.ExitState();

        //Debug.Log("Exiting AttackState_Turret");
    }
    public override void Step()
    {
        base.Step();

        shotSequenceTimer += Time.deltaTime;
        //RotateTowardsPlayer();
        if (shotSequenceTimer > timeBetweenShotsInSequence)
        {
            FireShot();
            shotSequenceTimer -= timeBetweenShotsInSequence;
        }

        
    }


    protected virtual void FireShot()
    {
        Shoot();

        shotsFired++;
        if (shotsFired >= numberOfShots)
        {
            enemyBehaviorTurret.SwitchToCooldownState();
        }
    }

    protected virtual void Shoot()
    {
        GameObject projectile = enemyBehaviorTurret.GetProjectileFromPool;

        projectile.GetComponent<IProjectile>().SetMyStats(enemyBehaviorTurret.Damage, projectileSpeed);
        projectile.transform.position = attackPoint.position;
        projectile.transform.rotation = attackPoint.rotation;

        projectile.SetActive(true);
    }

    protected virtual void RotateTowardsPlayer()
    {
        //transform.LookAt(enemyBehavior.PlayerPosition);
        Vector3 towardsPlayer = enemyBehavior.PlayerPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(towardsPlayer, transform.up);

        //float rotateAmount = rotationSpeed * Time.deltaTime;
        //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateAmount);
        //transform.rotation = newRotation;

        transform.rotation = targetRotation;
    }
}
