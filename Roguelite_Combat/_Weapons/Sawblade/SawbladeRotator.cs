using UnityEngine;

public class SawbladeRotator : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SawbladeDamage bladeDamage1;
    [SerializeField] private SawbladeDamage bladeDamage2;
    [Header("Debug")]
    [SerializeField] private float axisRotationSpeed = 1.0f;
    [Space(10)]
    [Header("Settings")]
    [SerializeField] private float axisRotationSpeed1 = 1.0f;
    [SerializeField] private float axisRotationSpeed2 = 10.0f;
    [SerializeField] private float axisRotationSpeed3 = 100.0f;




    private void Update()
    {
        float axisRotStep = axisRotationSpeed * Time.deltaTime;
        transform.Rotate(0.0f, axisRotStep, 0.0f);

    }


    public void SetRotationSpeed(int speedSetting)
    {
        if (speedSetting < 1)
            speedSetting = 1;
        if (speedSetting > 3)
            speedSetting = 3;

        switch (speedSetting)
        {
            case 1:
                axisRotationSpeed = axisRotationSpeed1;
                bladeDamage1.SetRotationSpeed(speedSetting);
                bladeDamage2.SetRotationSpeed(speedSetting);
                break;
            case 2:
                axisRotationSpeed = axisRotationSpeed2;
                bladeDamage1.SetRotationSpeed(speedSetting);
                bladeDamage2.SetRotationSpeed(speedSetting);
                break;
            case 3:
                axisRotationSpeed = axisRotationSpeed3;
                bladeDamage1.SetRotationSpeed(speedSetting);
                bladeDamage2.SetRotationSpeed(speedSetting);
                break;
        }

        Debug.Log("Rotation speed settings changed to " + speedSetting);
    }
 
}
