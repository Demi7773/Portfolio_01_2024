using UnityEngine;
using UnityEngine.UI;

public class FloatingHPBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBG;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraTransform;



    private void Update()
    {
        transform.position = targetTransform.position + offset;
        hpBG.transform.position = transform.position;
        transform.rotation = cameraTransform.rotation;
        hpBG.transform.rotation = transform.rotation;
    }

    public void HPBarUpdate(float ratio)
    {
        hpBar.fillAmount = ratio;
    }
}
