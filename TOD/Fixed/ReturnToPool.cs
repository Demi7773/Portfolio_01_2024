using DG.Tweening;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private Transform parentTransform;
    private float deactivateTimer;
    [SerializeField] float activeTime;

    [SerializeField] bool isAimCircle = false;//za fade aim circle sprite-a. izgleda malo bolje
    public SpriteRenderer forAimCircle;
    public Color forAimCircleColor;


    private void Start()
    {
        if (isAimCircle)
        {
            forAimCircle = gameObject.GetComponent<SpriteRenderer>();
            forAimCircleColor = forAimCircle.color;

            //print(forAimCircleColor);
        }
    }

    private void OnEnable()
    {
        if (isAimCircle)
        {
            forAimCircle = gameObject.GetComponent<SpriteRenderer>();
            forAimCircle.color = forAimCircleColor;
        }
    }

    private void Update()
    {
        deactivateTimer += Time.deltaTime;

        if (isAimCircle)
        {
            forAimCircle.DOColor(Color.clear, 1);
        }

        if (deactivateTimer >= activeTime)
        {
            transform.parent = parentTransform;
            deactivateTimer = 0;
            this.gameObject.SetActive(false);
        }
    }

    public void SetParentTransform(Transform parentObj)
    {
        parentTransform = parentObj;
    }

    public void SetActiveTime(float newValue)
    {
        activeTime = newValue;
    }
}
