using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerEvents;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Serializable]
    private class LevelNameId
    {
        public string SceneName;
        public LevelName LevelName;
    }

    [Header("Scenes")]
    [SerializeField] private List<LevelNameId> _levelNameIds = new List<LevelNameId>();

    private string _additivelyLoadedSceneName = string.Empty;

    // Ja dodao, trebalo bi iskljucit main menu scenu u pocetku ako radi kako mislim da oce
    //private string _additivelyLoadedSceneName = "MainMenu";


   // [SerializeField] private GameObject chooseLevelPanel;




    private void OnEnable()
    {
        RegionScreenEvents.RegionScreenButtonEvent += OnRegionScreenButtonEvent;
        PlayerEvents.PlayerGO += SetPlayerReference;
    }   

    private void OnDisable()
    {
        RegionScreenEvents.RegionScreenButtonEvent -= OnRegionScreenButtonEvent;
        PlayerEvents.PlayerGO -= SetPlayerReference;
    }

    private void SetPlayerReference(PlayerGOReference Player)
    {
        //Debug.Log("Player reference set in SceneMngmt");
        player = Player.playerGO;
    }

    private void OnRegionScreenButtonEvent(RegionScreenButtonEventData eventData)
    {
        LevelName requestedLevelName = eventData.LevelName;
        string sceneName = string.Empty;

        foreach(var levelNameId in _levelNameIds)
        {
            if(levelNameId.LevelName.Equals(requestedLevelName)) 
            {
                sceneName = levelNameId.SceneName;            
            }
        }

        if(sceneName != string.Empty) 
        {
            LoadScene(sceneName);
        }
    }

    private void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingDelay(sceneName));
        // TEST***********************************************
        //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainMenu"));
        //PlayerEvents.NeedPlayerReference?.Invoke();
        ////player.GetComponentInChildren<Camera>().enabled = true;
        //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(0));
        //Debug.Log("Player moved to scene: " + SceneManager.GetSceneByBuildIndex(0));

        //if (_additivelyLoadedSceneName != string.Empty)
        //{
        //    UnloadScene();
        //}

        ////chooseLevelPanel.SetActive(false);
        //_additivelyLoadedSceneName = sceneName;
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        //PlayerEvents.NeedPlayerReference?.Invoke();
        //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
        //Debug.Log("Player moved to scene: " + sceneName);
    }

    private void UnloadScene()
    {
        if (_additivelyLoadedSceneName == string.Empty)
        {
            return;
        }

        SceneManager.UnloadSceneAsync(_additivelyLoadedSceneName);
    }    

    IEnumerator LoadingDelay(string sceneName)
    {
        PlayerEvents.NeedPlayerReference?.Invoke();
        //Debug.Log("NeedPlayerReference Invoke");
        //player.GetComponentInChildren<Camera>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Waited 0.5");
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(0));
        Debug.Log("Player moved to scene: " + SceneManager.GetSceneByBuildIndex(0));
        PlayerEvents.NeedPlayerReference?.Invoke();

        yield return new WaitForSeconds(0.5f);
        if (_additivelyLoadedSceneName != string.Empty)
        {
            UnloadScene();
        }

        //chooseLevelPanel.SetActive(false);
        _additivelyLoadedSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        yield return new WaitForSeconds(2f);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
        Debug.Log("Player moved to scene: " + sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));



        PlayerEvents.NeedPlayerReference?.Invoke();
    }
}