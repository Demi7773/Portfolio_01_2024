using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class State : MonoBehaviour
{
    [SerializeField] protected EnemyBehavior enemyBehavior;
    [SerializeField] protected GameObject player;



    public virtual void EnterState(EnemyBehavior enemy, GameObject playerGO)
    {
        enemyBehavior = enemy;
        player = playerGO;
    }
    public virtual void Step()
    {

    }
    public virtual void ExitState()
    {
        StopAllCoroutines();
    }



    protected virtual void RotateTowardsTargetPositionWithRotateSpeed(Transform target)
    {
        //float rotationZ = (transform.rotation.eulerAngles.z - targetDirection.z) * enemyBehavior.RotateSpeed * Time.deltaTime;
        //Vector3 rotationStep = Vector3.RotateTowards(transform.up, targetDirection, enemyBehavior.RotateSpeed * Time.deltaTime, 1f);

        //transform.Rotate(transform.forward, rotationZ);



        transform.up = target.position - transform.position;

            // Need to figure out interpolation for turning here

        // Version from Aim Script
        //float angleRadians = Mathf.Atan2(target.position.y, target.position.x);

        //float angleDegrees = angleRadians * Mathf.Rad2Deg;
        //float angleDegreesNormalized = angleDegrees * enemyBehavior.RotateSpeed * Time.deltaTime;
        //Debug.Log("Degrees Rotate: " + angleDegreesNormalized);
        //transform.Rotate(0f, 0f, angleDegreesNormalized /*- 90f*/);


        //transform.rotation = Quaternion.Euler(0f, 0f, angleDegreesNormalized /*- 90f*/);
        //Debug.Log("RotationZ change: " + angleDegrees);

        Debug.DrawRay(transform.position, transform.up, Color.magenta);
    }
    protected virtual void MoveForwardStep()
    {
        float step = Time.deltaTime * enemyBehavior.PatrolSpeed;
        Vector3 positionChange = step * transform.up;
        //Debug.Log("Step distance: " + step + ", positionChange: " + positionChange);

        transform.position += positionChange;
    }


    protected virtual float DistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance from Player " + distance);
        return distance;
    }
    protected virtual Vector3 TowardsPlayerDirection()
    {
        Vector3 direction = player.transform.position - transform.position;
        //Debug.DrawRay(transform.position, direction, color: Color.red);
        return direction;
    }
}
