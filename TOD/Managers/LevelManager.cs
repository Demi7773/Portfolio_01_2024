using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] AvailableItems availableItems;
    [SerializeField] List</*EnemyBehaviour*/GameObject> enemies = new();
    [SerializeField] int enemiesAlive;
    [SerializeField] int enemiesKilled;
    [SerializeField] int targetKills;
    //[SerializeField] public WeatherSystem weatherSystem;



    private void OnEnable()
    {
        enemiesAlive = enemies.Count;
        MovePlayerToSpawnPoint();
        LevelManagerEvents.EnemyCountTrackerForLevelManagerEvent += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        LevelManagerEvents.EnemyCountTrackerForLevelManagerEvent -= OnEnemyDestroyed;
    }

    private void Start()
    {
        EnableAllEnemies();


        //weatherSystem.windDirection = transform.forward;
        int killsLeft = targetKills - enemiesKilled;
        HUDEvents.EnemyCountEvent?.Invoke(new EnemyCountData(/*enemiesAlive*/killsLeft));
        PlayerEvents.LevelStart?.Invoke();
        MovePlayerToSpawnPoint();

        Debug.Log("Invoking LevelStart");
    }

    private void MovePlayerToSpawnPoint()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.MovePosition(spawnPoint.position);
        rb.MoveRotation(spawnPoint.rotation);
        //player.position = spawnPoint.position;
        //player.rotation = spawnPoint.rotation;
    }



    // DEVBUILD TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            WinCondition();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            enemiesAlive = 0;
            enemiesKilled = 999;
            WinCondition();
        }
        if (Input.GetKey(KeyCode.I))
        {
            MovePlayerToSpawnPoint();
        }
    }



    private void OnEnemyDestroyed()
    {
        enemiesAlive--;
        enemiesKilled++;
        HUDEvents.EnemyCountEvent?.Invoke(new EnemyCountData(enemiesAlive));
        WinCondition();
    }

    private void WinCondition()
    {
        if (enemiesAlive <= 0)
        {
            DisableAllEnemies();

            //Victory screen event -> pick reward
            Debug.Log("WinCondition true");
            AudioEvents.PlayVictoryThemeEvent?.Invoke();
            ShopEvents.LevelRewardItemsEvent?.Invoke(new SendRewardsItemData(availableItems.endOfLevelReward));
            RaisePanelsFromLevelsEvents.RaiseVictoryPanelEvent?.Invoke();

            HUDEvents.HudToggleEvent?.Invoke(new HUDToggleData(false));
        }
        else if (enemiesKilled >= targetKills)
        {
            DisableAllEnemies();

            //Victory screen event -> pick reward
            Debug.Log("WinCondition true");
            AudioEvents.PlayVictoryThemeEvent?.Invoke();
            ShopEvents.LevelRewardItemsEvent?.Invoke(new SendRewardsItemData(availableItems.endOfLevelReward));
            RaisePanelsFromLevelsEvents.RaiseVictoryPanelEvent?.Invoke();

            HUDEvents.HudToggleEvent?.Invoke(new HUDToggleData(false));
        }
        else
        {
            Debug.Log("WinCondition false");
            Debug.Log("Game Over");
           //RaisePanelsFromLevelsEvents.RaiseDefeatPanelEvent?.Invoke();
            //PlayerEvents.GameOver?.Invoke();
        }
    }
    private void EnableAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<EnemyBehaviour>() != null)
            {
                enemy.GetComponent<EnemyBehaviour>().enabled = true;
            }
            else
            {
                Debug.Log("EnemyBehavior null!");
            }
        }
    }
    private void DisableAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<EnemyBehaviour>() != null)
            {
                enemy.GetComponent<EnemyBehaviour>().enabled = false;
            }
            else
            {
                Debug.Log("EnemyBehavior null!");
            }
        }
    }
}

[System.Serializable]
public class WeatherSystem
{
    [SerializeField] public int windSpeed;
    [SerializeField] public Vector3 windDirection;
}

