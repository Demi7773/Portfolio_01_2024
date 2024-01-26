using UnityEngine;
using DG.Tweening;

public class PlayerStaggerState : PlayerState
{
    [SerializeField] protected float timeSinceStart = 0.0f;
    [SerializeField] protected float staggerDuration = 0.3f;

    [Space(20)]
    [Header("Shake on Damage Animation")]
    [SerializeField] protected ShakeOnDamage shakeOnDmgEffect;
    //[SerializeField] private Transform playerModel;
    //[SerializeField] private float shakeDuration = 0.2f;
    //[SerializeField] private float shakeStrength = 1.0f;



    public override void EnterState(PlayerStateMachine stateMachine)
    {
        base.EnterState(stateMachine);
        Debug.Log("Entering StaggerState");
        timeSinceStart = 0.0f;
        shakeOnDmgEffect.Shake();
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting StaggerState");
        shakeOnDmgEffect.ResetSize();
    }
    public override void Step()
    {
        base.Step();

        timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= staggerDuration)
        {
            playerStateMachine.SwitchToDefaultState();
        }
    }
}
