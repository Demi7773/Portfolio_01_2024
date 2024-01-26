using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] protected bool isPaused = false;
    [SerializeField] protected LevelManager levelManager;
    [SerializeField] protected GameObject player;
    [SerializeField] protected EnemySpawnPoints spawnPoints;

    [Space(20)]
    [Header("Current Stats Tracking")]
    [SerializeField] protected float timer = 0.0f;
    [SerializeField] protected bool isSpawning = false;
    [SerializeField] protected int totalEnemiesSpawned = 0;
    [SerializeField] protected Queue<GameObject> enemiesQueue = new Queue<GameObject>();
    [Space(20)]
    [Header("Settings")]
    [Space(10)]
    [Header("Pool")]
    [SerializeField] protected Transform poolSpawnPos;
    [SerializeField] protected int poolSize = 100;
    //[SerializeField] protected float radiusAroundPlayer;
    [Space(10)]
    [Header("Enemy")]
    [SerializeField] protected GameObject enemyPrefab;
    [Space(10)]
    [Header("Level")]
    [SerializeField] protected int targetSpawns = 100;
    [SerializeField, Range(0.01f, 100f)] protected float delayBetweenSpawns = 1f;
    [SerializeField, Range(0.01f, 100f)] protected float delayBeforeFirstSpawn = 1f;


    [SerializeField] protected List<Transform> spawnPositionsAroundPlayer = new List<Transform>();





        // Controlled by LevelManager
    public void InitializeMe(LevelManager lvlManager, GameObject playerRef, EnemySpawnPoints spawnP)
    {
        levelManager = lvlManager;
        player = playerRef;
        spawnPoints = spawnP;
        InitializeEnemyPool();
        //Debug.Log("Enemy Spawner Initialized, deactivating in hierarchy");
        gameObject.SetActive(false);
    }
    public void StartSpawning()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(DelayBeforeFirstSpawn());
        Debug.Log("EnemySpawner started spawning");
    }
    public void StopSpawning()
    {
        StopAllCoroutines();
        isSpawning = false;
        Debug.Log("EnemySpawner stopped spawning");
        this.gameObject.SetActive(false);
    }



        // Event system
    protected void OnEnable()
    {
        
        //StartCoroutine("DelayBeforeFirstSpawn");
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



    protected void Update()
    {
        if (!isPaused)
        {
            if (isSpawning)
            {
                timer += Time.deltaTime;
                if (timer >= delayBetweenSpawns)
                {
                    SpawnEnemyFromPool();
                    timer -= delayBetweenSpawns;
                }
            }
        }
    }



    protected void InitializeEnemyPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, poolSpawnPos.position, Quaternion.identity, poolSpawnPos);
            InitializeEnemyScript(newEnemy);
            AddEnemyToPool(newEnemy);
        }
        //Debug.Log("Enemy pool initialized from EnemySpawner");
    }
    protected void InitializeEnemyScript(GameObject newEnemy)
    {
        if (newEnemy.GetComponent<EnemyStupid>() != null)
        {
            newEnemy.GetComponent<EnemyStupid>().InitializeEnemy(this, player, levelManager);
        }
        else
        {
            Debug.LogError("Enemy script null!");
        }
    }

    protected void AddEnemyToPool(GameObject newEnemy)
    {
        newEnemy.SetActive(false);
        enemiesQueue.Enqueue(newEnemy);
    }
    public void ReturnEnemyToPool(GameObject deadEnemy)
    {
        AddEnemyToPool(deadEnemy);
    }

    protected void SpawnEnemyFromPool()
    {
        GameObject newEnemy = enemiesQueue.Dequeue();
        newEnemy.transform.position = spawnPoints.GetRandomSpawnPointFromList().position;
        newEnemy.SetActive(true);

        totalEnemiesSpawned++;
        if (totalEnemiesSpawned >= targetSpawns)
        {
            Debug.Log("Spawn Target reached: " + targetSpawns + ", stopped spawning");
            isSpawning = false;
        }

        Debug.Log("Enemy spawned");

        ExpandPoolIfRunningEmpty();
    }
    protected void ExpandPoolIfRunningEmpty()
    {
        if (enemiesQueue.Count < 10)
        {
            InitializeEnemyPool();
        }
    }



    protected IEnumerator DelayBeforeFirstSpawn()
    {
        yield return new WaitForSeconds(delayBeforeFirstSpawn);
        Debug.Log("Spawner Started Spawning");
        isSpawning = true;
        timer = delayBetweenSpawns;
    }
}
