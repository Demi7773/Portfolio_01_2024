using TOD.Statemachine;
using UnityEngine;

[CreateAssetMenu(fileName ="SP_Rammer",menuName ="StateMachine/Special Attacks/SP_Rammer")]
public class SpecialAttacks_Rammer : State
{
    [SerializeField] float ramSpeed;
    [SerializeField] float ramTime;
    [SerializeField] State cooldownState;
    public float timer;
    public float targetWindowTime;
    public Vector3 playerDirection;

    float interpolationTimer;
    public override void Think(EnemyBehaviour enemy)
    {
        timer += Time.deltaTime;

        if (timer >= ramTime)
        {
            timer = 0;
            enemy.enemyState = cooldownState;
        }

        PlayerPosition(enemy);
        //base.Think(enemy);

    }

    private void PlayerPosition(EnemyBehaviour enemy)
    {
        interpolationTimer += Time.deltaTime;

        Vector3 direction = (enemy.playerTransform.position - enemy.transform.position).normalized;

        Quaternion rotationToWayPoint = Quaternion.LookRotation(direction);

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotationToWayPoint, interpolationTimer);



        enemy.GetComponent<Rigidbody>().velocity = enemy.transform.forward * Time.deltaTime * ramSpeed;
 

    }

    //void TargetWindow(EnemyBehaviour enemy)
    //{
    //    timer += Time.deltaTime;
    //    if (timer < targetWindowTime)
    //    {
    //        playerDirection = enemy.playerPostion.position - enemy.transform.position;
    //        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //    else
    //    {
    //        ChargePlayer(enemy);
    //    }
    //}

    //void ChargePlayer(EnemyBehaviour enemy)
    //{
    //    interpolationTimer += Time.deltaTime;
        
    //    if (enemy.transform.forward == playerDirection)
    //    {
    //        enemy.GetComponent<Rigidbody>().velocity = enemy.transform.forward * Time.deltaTime * ramSpeed;
    //    }
    //    else
    //    {
    //        Quaternion rotationToWayPoint = Quaternion.LookRotation(playerDirection);
    //        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotationToWayPoint, interpolationTimer);
            
    //        //timer = 0;
    //        //interpolationTimer = 0;
    //    }
    //}

}
