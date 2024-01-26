using DG.Tweening;
using TOD.Statemachine;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Attack")]
public class AttackState : State
{
    //[SerializeField] State cooldownState;
   // [SerializeField] SpriteRenderer aimSprite;
    [Space(10)]
    [SerializeField] int numberOfShots = 5;
    [Space(10)]
    [SerializeField] float height = 9;
    [SerializeField] float gravity = Physics.gravity.y;
    [Space(10)]
    [SerializeField] float aimOffset = 6;
    [SerializeField] float leadShootValue = 0;

    private Vector3 impactPoint;
    private int check;



    public override void Think(EnemyBehaviour enemy)
    {
        //SpriteRenderer target = Instantiate(aimSprite);
        enemy.GetComponent<NavMeshAgent>().speed = 0;


        if (enemy.playerTransform != null)
        {
            impactPoint = enemy.playerTransform.position + (enemy.playerTransform.forward * leadShootValue);
            //target.transform.position = enemy.playerTransform.position+(enemy.playerTransform.forward*leadShootValue);

            Debug.Log("Impact point: " +  impactPoint);
            enemy.enemyCannonObjectPool.FetchAimReticle(impactPoint);
            //target.transform.position = impactPoint;
            Attack(enemy, impactPoint);
        }

        else
        {
            PlayerEvents.NeedPlayerReference?.Invoke();
            Debug.Log("Player reference null!");
        } 
    }

    void Attack(EnemyBehaviour enemy,Vector3 impactPoint)
    {
        enemy.enemyCannonObjectPool.FetchPooledSmoke(enemy.shootPosition.position);
        AudioEvents.PlayCannonSoundsEvent?.Invoke();
        Debug.Log("Enemy Attack");

        for (int i = 0; i < numberOfShots; i++)
        {
            var projectile = enemy.enemyCannonObjectPool.FetchEnemyCannonball(enemy.shootPosition.position);

            if (projectile == null)
            {
                enemy.GoToCooldownState();
                //enemy.enemyState = cooldownState;
            }

            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = CalculateLauncVelocity(enemy,impactPoint);
        }

        enemy.transform.DOShakeScale(0.5f, 0.5f);

        //enemy.enemyState = cooldownState;
        enemy.GoToCooldownState();
        Debug.Log("Switching to CooldownState");
    }

    Vector3 CalculateLauncVelocity(EnemyBehaviour enemy,Vector3 impactPoint) //Sebastian Lague
    {
        float displacmentY = enemy.playerTransform.position.y - enemy.transform.position.y;
        Vector3 displacmentXZ = new Vector3(
            Random.Range(impactPoint.x - aimOffset, impactPoint.x + aimOffset) - enemy.transform.position.x, 
            0, 
            Random.Range(impactPoint.z - aimOffset, impactPoint.z + aimOffset) - enemy.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacmentXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacmentY - height) / gravity));
        return velocityXZ + velocityY;
    }
}
