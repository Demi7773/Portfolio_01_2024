using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraSmoothing;

    private void Update()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing * Time.deltaTime);



        //Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, cameraSmoothing * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, smoothedRotation.y, transform.rotation.z);
        //transform.position = smoothedPostion;
        transform.LookAt(playerTransform);

        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(transform.up);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(-transform.up);
        }

    }

    public void CamSwitch(bool onOff)
    {
        gameObject.SetActive(onOff);
    }

}
