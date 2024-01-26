using UnityEngine;

public class AimDetection : MonoBehaviour
{
    [SerializeField] protected Transform[] sensors;



    public virtual bool IsAimingAtTarget(float range, LayerMask layers)
    {
        foreach (Transform t in sensors)
        {
            Debug.DrawLine(t.position, t.forward *  range, Color.red, 1.0f);
            if (Physics.Raycast(t.position, t.forward, range, layers))
            {
                return true;
            }
        }

        return false;
    }



    //private void OnDrawGizmosSelected()
    //{
    //    foreach (Transform t in sensors)
    //    {
    //        Gizmos.DrawLine(t.position, t.forward);
    //    }
    //}
}
