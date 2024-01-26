using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    //LevelManager levelManager;
    Rigidbody playerRigidbody;

    [SerializeField] float shipSpeed;
    [SerializeField] float shipWindSpeedAdded;
    [SerializeField] float shipTurnRadius;

    [SerializeField] EquipmentController equipmentController;

    [SerializeField] KeyCode rotateRight;
    [SerializeField] KeyCode rotateLeft;

    public static UnityAction<PlayerMovement> PlayerRefrence;

    private void Awake()
    {
        equipmentController.CalculateModifiers();
    }
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //levelManager = FindAnyObjectByType<LevelManager>();
        PlayerRefrence?.Invoke(this);

        shipSpeed = equipmentController.PlayerSpeedStat;
        shipTurnRadius = equipmentController.PlayerTurnStat;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerInput();
       // AddedWindSpeed();
    }

    //private void AddedWindSpeed()
    //{
    //    float angelBetween = Vector3.Angle(transform.forward, levelManager.weatherSystem.windDirection);
    //    angelBetween = Mathf.Deg2Rad * angelBetween;
    //    angelBetween = Mathf.Cos(angelBetween);

    //    shipWindSpeedAdded = levelManager.weatherSystem.windSpeed * angelBetween;
    //    //Debug.Log(angelBetween);
    //}

    private void FixedUpdate()
    {
        playerRigidbody.AddForce( transform.forward * Time.deltaTime * (shipSpeed + equipmentController.PlayerSpeedStat + shipWindSpeedAdded));
        playerRigidbody.AddTorque(PlayerInput(), ForceMode.Force);
    }

    Vector3 PlayerInput()
    {
        if (Input.GetKey(rotateRight))
        {
            return new Vector3(0, shipTurnRadius * Time.deltaTime, 0);
        }
        else if (Input.GetKey(rotateLeft))
        {
            return new Vector3(0, -shipTurnRadius * Time.deltaTime, 0);
        }

        // ja dodao else, test
        else
            return Vector3.zero;
    }

    public float GetSpeed()
    {
        return shipSpeed + shipWindSpeedAdded;
    }

    public float ReturnPlayerSpeed()
    {
        return shipSpeed;
    }


    public void HeavyCannonBallHit(float value)
    {
        StartCoroutine(HeavyHit(value));
    }

    public IEnumerator HeavyHit(float hitStun)
    {
        float originalSpeed = shipSpeed;
        shipSpeed = 0;
        yield return new WaitForSeconds(hitStun);
        shipSpeed = originalSpeed;
        StopCoroutine("HeavyHit");
    }


}
