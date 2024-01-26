using UnityEngine;

public class EnemyBehavior_Sawblade : EnemyBehavior
{
    [Header("Sawblade Specific")]
    [SerializeField] protected SawbladeRotator rotator;
    //[SerializeField] protected float rotateBladesSpeed;

    public SawbladeRotator Rotator => rotator;


}