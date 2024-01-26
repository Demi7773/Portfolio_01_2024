using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEvents;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private int numberOfRunsTotal = 0;
    [SerializeField] private int currentLevel = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private EquipmentController equipmentController;

    [SerializeField] private Button firstLevelButton;
    //[SerializeField] private SceneManagement sceneManagement;



    public int CurrentLevel => currentLevel;
    public GameObject Player => player;

    private void OnEnable()
    {
        PlayerGO += SetPlayerReference;
        LevelStart += OnEnterLevel;
        PlayerEvents.GameOver += GameOver;
    }
    private void OnDisable()
    {
        PlayerGO -= SetPlayerReference;
        LevelStart -= OnEnterLevel;

        StopAllCoroutines();
    }

    private void Start()
    {
        //PlayerEvents.PlayerGO?.Invoke(new PlayerGOReference(player));
        //OnStartNewRun(player);
    }


    private void GameOver()
    {
        Debug.Log("GAME OVER");
        isGameOver = true;
        currentLevel = 0;
    }



    private void SetPlayerReference(PlayerGOReference Player)
    {
        player = Player.playerGO;
    }

    private void OnEnterLevel()
    {
        // dodat za provjeru new run, ili negdje drugdje dodat
        // dodat sistem za ovo
        //Debug.Log("Level start in GM");
        //PlayerEvents.PlayerGO?.Invoke(new PlayerGOReference(player));
        //PlayerEvents.NeedPlayerReference?.Invoke();

        currentLevel++;


        
        if (player != null)            
        {
            //player.GetComponent<CameraController>().CamSwitch(true);
        }
        else
        {
            PlayerEvents.NeedPlayerReference?.Invoke();
            StartCoroutine(WaitThenSwitchCamOn());
        }
    }

    private IEnumerator WaitThenSwitchCamOn()
    {
        yield return new WaitForSeconds(0.5f);

        if (player != null)
        {
            player.GetComponent<CameraController>().CamSwitch(true);
        }
        else
        {
            Debug.Log("Player still null somehow??");
        }
        StopAllCoroutines();
    }

    private void OnLeaveLevel()
    {

    }


    private void Awake()
    {
        if (PlayerPrefs.HasKey("totalRuns"))
        {
            numberOfRunsTotal = PlayerPrefs.GetInt("totalRuns");
        }
    }


    public void PauseOrUnpause (bool setActive)
    {

    }

    // Spojeno na ChooseButtonShip
    public void OnStartNewRun(GameObject playerRef)
    {
        numberOfRunsTotal++;
        firstLevelButton.interactable = true;
        
        PlayerPrefs.SetInt("totalRuns", numberOfRunsTotal);
        if (player == null)
        {
            PlayerEvents.NeedPlayerReference?.Invoke();
        }
        playerRef.SetActive(true);
        equipmentController.EquipAllStartingItems();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
