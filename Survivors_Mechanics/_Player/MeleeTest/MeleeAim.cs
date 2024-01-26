using UnityEngine;
using static PlayerEvents;

public class MeleeAim : MonoBehaviour
{
    //[Header("Set in Inspector")]
    //[SerializeField] protected GameObject rotator;
    //[SerializeField] protected float rotationSpeed;
    [SerializeField] protected bool isPaused;
    [SerializeField] protected Camera topViewCam;



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
    }
    private void OnDisable()
    {
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
    }
    private void PauseMe()
    {
        isPaused = true;
    }
    private void UnPauseMe()
    {
        isPaused = false;
    }



    protected void Update()
    {
        if (!isPaused)
        {
            RotateToMouse();
        }
    }

    protected void RotateToMouse()
    {
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = topViewCam.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = rotator.transform.position.z;
        mousePosition.y = transform.position.y;

        Vector3 direction = mousePosition - transform.position;
        float angleRadians = Mathf.Atan2(direction.x, direction.z);

        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angleDegrees, 0f);

        //Debug.DrawRay(rotator.transform.position, rotator.transform.forward, Color.green);
        Debug.DrawLine(transform.position, transform.forward * 100f, Color.green);
    }
}
