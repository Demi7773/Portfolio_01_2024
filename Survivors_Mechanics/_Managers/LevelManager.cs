using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class LevelManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawnPoints enemySpawnpoints;
    [Space(20)]

    [SerializeField] private GameObject expPickupPrefab;
    [SerializeField] private Transform expPoolParent;
    [SerializeField] private int expPickupsPoolSize = 500;
    [SerializeField] private Queue<GameObject> expPickupsQueue = new Queue<GameObject>();

    [Space(20)]
    [Header("Current Stats")]
    [SerializeField] private int currentPhase = 0;
    [SerializeField] private float currentLevelTimer = 0.0f;

    [Space(20)]
    [Header("Settings for Phases")]
    [SerializeField] private float delayBeforePhase1 = 1.0f;
    [SerializeField] private EnemySpawner enemySpawner1;
    [Space(10)]
    [SerializeField] private float startPhase2Time = 60.0f;
    [SerializeField] private EnemySpawner enemySpawner2;
    [Space(10)]
    [SerializeField] private float startPhase3Time = 150.0f;
    [SerializeField] private EnemySpawner enemySpawner3;
    [Space(10)]
    [SerializeField] private float startPhase4Time = 300.0f;






    public int CurrentPhase => currentPhase;
    public float CurrentLevelTimer => currentLevelTimer;





    protected void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
    }
    protected void OnDisable()
    {
        StopAllCoroutines();
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
    }
    protected void PauseMe()
    {
        isPaused = true;
    }
    protected void UnPauseMe()
    {
        isPaused = false;
    }





    private void Start()
    {
        currentPhase = 0;
        StartCoroutine(StartLevelSequence());
    }
    private void Update()
    {
        if (!isPaused)
        {
            currentLevelTimer += Time.deltaTime;
            Phases();
        }
    }
    private void Phases()
    {
        switch (currentPhase)
        {
            case 0:
                currentLevelTimer = 0.0f;
                break;

            case 1:
                if (currentLevelTimer < startPhase2Time)
                    break;
                else
                    SwitchToNewPhase(currentPhase);
                    break;

            case 2:
                if (currentLevelTimer < startPhase3Time)
                    break;
                else
                    SwitchToNewPhase(currentPhase);
                    break;

            case 3:
                if (currentLevelTimer < startPhase4Time)
                    break;
                else
                    SwitchToNewPhase(currentPhase);
                    break;

            case 4:
                Debug.Log("Add behaviour for phase4");
                break;
        }
    }
    private void SwitchToNewPhase(int currentPhaseToSwap)
    {
        currentPhaseToSwap++;
        currentPhase = currentPhaseToSwap;

        SetSpawnerForPhase(currentPhase);

        Debug.Log("Phase " + currentPhase + " started");
    }
    private void SetSpawnerForPhase(int newPhase)
    {
        switch (currentPhase)
        {
            case 0:
                break;

            case 1:
                enemySpawner1.StartSpawning();
                break;

            case 2:
                enemySpawner1.StopSpawning();
                enemySpawner2.StartSpawning();
                break;

            case 3:
                enemySpawner2.StopSpawning();
                enemySpawner3.StartSpawning();
                break;

            case 4:
                Debug.Log("Add behaviour for phase4");
                break;
        }
    }




        // Level Start and Level End
    private IEnumerator StartLevelSequence()
    {
        InitializeEnemySpawners();
        InitializeExpPickupsPool();

        yield return new WaitForSeconds(delayBeforePhase1);

        //currentLevelTimer = 0.0f;
        currentPhase = 0;
        SwitchToNewPhase(currentPhase);

        LevelStart?.Invoke();
        SetHUD?.Invoke(new OnOrOff(true));
        InvokeRepeating("InvokeTimerTickEvent", 0.0f, 1.0f);
    }
    private void InvokeTimerTickEvent()
    {
        TimerTick?.Invoke();
    }


        // EXP Pickups Pooling
    private void InitializeExpPickupsPool()
    {
        for (int i = 0; i < expPickupsPoolSize; i++)
        {
            GameObject expPickup = Instantiate(expPickupPrefab, expPoolParent);

            if (expPickup.GetComponent<IEXP>()  != null)
            {
                expPickup.GetComponent<IEXP>().SetMyPool(this);
            }
            else
            {
                Debug.LogError("IEXP null on instance!");
            }

            expPickup.SetActive(false);
        }
    }
    public void ReturnEXPPickupToPool(GameObject pickup) 
    {
        expPickupsQueue.Enqueue(pickup);
    }
    public GameObject GetEXPPickupFromPool()
    {
        CheckIfRunningEmpty();
        return expPickupsQueue.Dequeue();
    }
    private void CheckIfRunningEmpty()
    {
        if (expPickupsQueue.Count > 100)
        {
            InitializeExpPickupsPool();
        }
    }



    private void InitializeEnemySpawners()
    {
        enemySpawner1.InitializeMe(this, player, enemySpawnpoints);
        enemySpawner2.InitializeMe(this, player, enemySpawnpoints);
        enemySpawner3.InitializeMe(this, player, enemySpawnpoints);
    }



    private IEnumerator LevelEndSequence()
    {
        yield return new WaitForSeconds(1f);
        SetHUD?.Invoke(new OnOrOff(false));
        LevelEnd?.Invoke();
    }


}
