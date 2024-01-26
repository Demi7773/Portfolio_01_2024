using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] protected PlayerStateMachine playerStateMachine;
    [SerializeField] protected PlayerStats stats;


    public virtual void EnterState(PlayerStateMachine stateMachine)
    {
        playerStateMachine = stateMachine;
    }
    public virtual void ExitState()
    {

    }
    public virtual void Step()
    {

    }
}
