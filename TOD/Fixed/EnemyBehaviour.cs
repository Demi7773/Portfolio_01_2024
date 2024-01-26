using System.Collections.Generic;
using TOD.Statemachine;
using UnityEngine;
using UnityEngine.AI;
using static PlayerEvents;

[RequireComponent(typeof(NavMeshAgent))]/*,typeof(Rigidbody),typeof(EnemyCannonObjectPool))*/
public class EnemyBehaviour : MonoBehaviour, IEnemy
{
    [Header("StateMachine")]
    public State enemyState;

    [SerializeField] private PatrolState patrolState;
    [SerializeField] private AttackState attackState;
    [SerializeField] private CooldownState cooldownState;
    [SerializeField] private ChaseState chaseState;

    public float cooldownTimer;



    [Space(30)]
    [Header("Waypoints")]
    public Waypoints waypoints;

    [Tooltip("For Debugging")]
    public int currentWaypoint = 0;

    [Space(10)]
    [Header("Stats")]
    public EnemyStats enemyStats;
    public float currentHP;
    public float dmgReduction;
    [SerializeField] private int goldValue = 10;

    [Space(10)]
    [Header("Dependencies")]
    public Transform playerTransform;
    public NavMeshAgent enemyNavMeshAgent;
    public Transform shootPosition;
    public EnemyCannonObjectPool enemyCannonObjectPool;

    [Header("Particle FX")]
    [SerializeField] private GameObject deadParticles;
    [SerializeField] private GameObject deadShipwreck;
    [SerializeField] private GameObject hitParticle;



    public void OnValidate()
    {
        waypoints.CastTransformToVector();
    }


    private void OnEnable()
    {
        //PlayerMovement.PlayerRefrence += PlayerRefrence;
        PlayerGO += PlayerReference;
    }
    private void OnDisable()
    {
        //PlayerMovement.PlayerRefrence -= PlayerRefrence;
        PlayerGO -= PlayerReference;
    }
    //private void PlayerRefrence(PlayerMovement player)
    //{
    //    playerTransform = player.gameObject.transform;
    //}
    private void PlayerReference(PlayerGOReference Player)
    {
        playerTransform = Player.playerGO.transform;
    }

    public void GoToPatrolState()
    {
        enemyState = patrolState;
    }
    public void GoToAttackState()
    {
        enemyState = attackState;
    }
    public void GoToCooldownState()
    {
        enemyState = cooldownState;
    }
    public void GoToChaseState()
    {
        enemyState= chaseState;
    }


    private void Start()
    {
        enemyCannonObjectPool.CreateObjectPool(transform);

        currentHP = enemyStats.enemyMaxHP;
        dmgReduction = enemyStats.dmgReduction;

        waypoints.CastTransformToVector();
        DrawPathLine();
        GoToPatrolState();
    }
    void DrawPathLine()
    {
        for (int i = 0; i < waypoints.point.Length; i++)
        {

            if ((i + 1) == waypoints.point.Length)
            {
                Debug.DrawLine(waypoints.point[i], waypoints.point[0], Color.white, Mathf.Infinity);
                return;
            }

            Debug.DrawLine(waypoints.point[i], waypoints.point[i + 1], Color.white, Mathf.Infinity);
        }
    }


    private void Update()
    {
        enemyState.Think(this);
    }


    public void LoseHP(float dmg)
    {
        dmg -= dmg * (dmgReduction / 100f);
        Instantiate(hitParticle, transform.position, transform.rotation);
        currentHP -= dmg;

        if (enemyState is PatrolState)
        {
            GoToAttackState();
        }

        if (currentHP <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        LevelManagerEvents.EnemyCountTrackerForLevelManagerEvent?.Invoke();
        
        if (playerTransform.GetComponent<PlayerMoneyScript>() != null)
        {
            playerTransform.GetComponent<PlayerMoneyScript>().GetMoney(goldValue);
        }
        else
        {
            Debug.Log("Player MoneyScript null");
        }

        Instantiate(deadParticles, transform.position, transform.rotation);
        Instantiate(deadShipwreck, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}



[System.Serializable]
public class Waypoints
{
    [SerializeField] Transform WaypointHolder;
    [SerializeField] Vector3[] _points;
    [SerializeField] int _numberOfWaypoints;

    public void CastTransformToVector()
    {
        _numberOfWaypoints = WaypointHolder.childCount;
        _points = new Vector3[_numberOfWaypoints];

        for (int i = 0; i < WaypointHolder.childCount; i++)
        {
            _points[i] = WaypointHolder.GetChild(i).transform.position;
            _points[i] = new Vector3(_points[i].x, 0, _points[i].z);
        }
    }
    public Vector3[] point => _points;
    public int numberofWaypoints => _numberOfWaypoints;
}


[System.Serializable]
public class EnemyCannonObjectPool
{
    [Header("Settings")]
    [SerializeField] private int poolCounCannonball;
    [SerializeField] private int poolCountSmoke = 5;
    [SerializeField] private int poolCountAim = 5;

    [Space(10)]
    [Header("Prefabs")]
    [SerializeField] private GameObject EnemyCannonballPrefab;
    [SerializeField] private GameObject SmokePrefab;
    [SerializeField] private GameObject AimReticlePrefab;

    private Transform parent;

    [Space(10)]
    [Header("Pools and Holders")]
    [SerializeField] private List<GameObject> EnemyCannonballPool = new List<GameObject>();
    [SerializeField] private List<GameObject> smokePool = new List<GameObject>();
    [SerializeField] private List<GameObject> aimReticlePool = new List<GameObject>();



    public void CreateObjectPool(Transform obj)
    {
        parent = obj;

        InitializePool(EnemyCannonballPool, EnemyCannonballPrefab);
        InitializeSmokePool();
        InitializeAimReticlePool();
    }

    private void InitializePool(List<GameObject> cannonballPool, GameObject cannonball)
    {
        GameObject temporaryCannonBall;
        for (int i = 0; i < poolCounCannonball; i++)
        {
            temporaryCannonBall = MonoBehaviour.Instantiate(cannonball, parent/*, cannonBallParent*/);
            temporaryCannonBall.GetComponent<ReturnToPool>().SetParentTransform(parent);
            temporaryCannonBall.SetActive(false);
            cannonballPool.Add(temporaryCannonBall);
        }
    }
    private void InitializeSmokePool()
    {
        GameObject temporarySmoke;
        for (int i = 0; i < poolCountSmoke; i++)
        {
            temporarySmoke = MonoBehaviour.Instantiate(SmokePrefab, parent/*, smokeParent*/);
            temporarySmoke.GetComponent<ReturnToPool>().SetParentTransform(parent);
            temporarySmoke.SetActive(false);
            smokePool.Add(temporarySmoke);
        }
    }
    private void InitializeAimReticlePool()
    {
        GameObject temporaryAimReticle;
        for (int i = 0; i < poolCountAim; i++)
        {
            temporaryAimReticle = MonoBehaviour.Instantiate(AimReticlePrefab, parent);
            temporaryAimReticle.GetComponent<ReturnToPool>().SetParentTransform(parent);
            temporaryAimReticle.SetActive(false);
            aimReticlePool.Add(temporaryAimReticle);
        }
    }



    public GameObject FetchEnemyCannonball(Vector3 position)
    {
        for (int i = 0; i < poolCounCannonball; i++)
        {
            if (!EnemyCannonballPool[i].activeInHierarchy)
            {
                EnemyCannonballPool[i].transform.parent = null;

                EnemyCannonballPool[i].transform.position = position;
                EnemyCannonballPool[i].SetActive(true);
                return EnemyCannonballPool[i];
            }
        }
        return null;
    }

    public GameObject FetchPooledSmoke(Vector3 position)
    {
        for (int i = 0; i < poolCountSmoke; i++)
        {
            if (!smokePool[i].activeInHierarchy)
            {
                smokePool[i].transform.parent = null;

                smokePool[i].transform.position = position;
                smokePool[i].SetActive(true);
                return smokePool[i];
            }
        }
        return null;
    }
    public GameObject FetchAimReticle(Vector3 position)
    {
        for (int i = 0; i < poolCountAim; i++)
        {
            if (!aimReticlePool[i].activeInHierarchy)
            {
                aimReticlePool[i].transform.parent = null;

                aimReticlePool[i].transform.position = position;
                aimReticlePool[i].SetActive(true);
                return aimReticlePool[i];
            }
        }
        return null;
    }
}