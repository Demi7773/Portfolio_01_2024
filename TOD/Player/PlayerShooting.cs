using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] KeyCode ShootLeft = KeyCode.Q;
    [SerializeField] KeyCode ShootRight = KeyCode.E;

    [SerializeField] EquipmentController equipmentController;

    [SerializeField] GameObject cannonholder;

    [SerializeField] Transform shootPointLeft;
    [SerializeField] Transform shootPointRight;

    [SerializeField] float cannonballArcHeight = 9;
    [SerializeField] float gravity = Physics.gravity.y;

    [SerializeField] bool canShoot = true;
    [SerializeField] float timeBetweenShots;

    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] float shakeStrength = 0.2f;

    [SerializeField] GameObject ShipModel;//refrenca na model, za DoShakeScale, ako stavim na Player object kamera se isto scale-a i izgleda cudno
    [SerializeField] float shipDoShakeDuration = 1f;
    [SerializeField] float shipDoShakeStrength = 1f;

    private void Start()
    {
        CreateCannonInstance();
    }

    private void OnEnable()
    {
        PlayerEvents.NewEquipment += CreateCannonInstance;
    }
    private void OnDisable()
    {
        PlayerEvents.NewEquipment -= CreateCannonInstance;
    }


    private void Update()
    {
        if (Input.GetKeyDown(ShootLeft) && canShoot)
        {
            Vector3 cannonBallVelocity = CalculateLauncVelocity(transform.right * -1);
            cannonholder.gameObject.GetComponent<ICannon>().ActivateCannon(cannonBallVelocity, shootPointLeft);
            canShoot = false;

            StartCoroutine(CooldownTimer(timeBetweenShots));
            transform.DOShakeScale(shakeDuration, shakeStrength);
            ShipModel.transform.DOShakeScale(shipDoShakeDuration, shipDoShakeStrength);//možda bolje izgleda kad pucamo
        }

        if (Input.GetKeyDown(ShootRight) && canShoot)
        {

            Vector3 cannonBallVelocity = CalculateLauncVelocity(transform.right);
            cannonholder.gameObject.GetComponent<ICannon>().ActivateCannon(cannonBallVelocity, shootPointRight);
            canShoot = false;
            ShipModel.transform.DOShakeScale(shipDoShakeDuration, shipDoShakeStrength);//možda bolje izgleda kad pucamo

            StartCoroutine(CooldownTimer(timeBetweenShots));
            transform.DOShakeScale(shakeDuration, shakeStrength);
        }
    }

    Vector3 CalculateLauncVelocity(Vector3 direction) //Sebastian Lague
    {
        float displacmentY = direction.y - direction.y;
        Vector3 displacmentXZ = new Vector3(direction.x * equipmentController.PlayerRangeStat * 2f, 0, direction.z * equipmentController.PlayerRangeStat * 2f);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * cannonballArcHeight);
        Vector3 velocityXZ = displacmentXZ / (Mathf.Sqrt(-2 * cannonballArcHeight / gravity) + Mathf.Sqrt(2 * (displacmentY - cannonballArcHeight) / gravity));
        return velocityXZ + velocityY;
    }

    void CreateCannonInstance()
    {
        if (cannonholder != null)
        {
            Destroy(cannonholder.gameObject);
        }

        cannonholder = Instantiate(equipmentController.PlayerCannon.CannonInstance, transform);

        // Promjenio sam ICannon da trazi ovu metodu. U metodi se Seta referenca na ScriptableObject od Cannona
        cannonholder.GetComponent<ICannon>().InitializeCannon(equipmentController.EquippedCannons);
        timeBetweenShots = cannonholder.GetComponent<ICannon>().Cooldown();
    }

    IEnumerator CooldownTimer(float timer)
    {
        AudioEvents.PlayCannonSoundsEvent?.Invoke();
        yield return new WaitForSeconds(timer);
        canShoot = true;
        StopCoroutine("CooldownTimer");
    }
}
