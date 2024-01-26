using UnityEngine;

public class AimModule : MonoBehaviour
{
    
    public float DirectionToTargetDOT(Vector3 direction, Vector3 targetDirection)
    {
        float dot = Vector3.Dot(direction, targetDirection.normalized);
        return dot;
    }

}
