using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioEvents;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels = new List<GameObject>();




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadLevelByIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevelByIndex(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevelByIndex(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadLevelByIndex(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadLevelByIndex(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadLevelByIndex(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadLevelByIndex(6);
        }
    }


    public void LoadLevelByIndex(int index)
    {
        Debug.Log("Loading level: " + index);

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }

        levels[index].SetActive(true);

        PlayLevelSoundTrack(index);
    }

    private void PlayLevelSoundTrack(int index)
    {
        switch (index)
        {
            case 0:
                PlayLevel1ThemeEvent?.Invoke();
                break;
            case 1:
                PlayShopThemeEvent?.Invoke();
                break;
            case 2:
                PlayLevel2ThemeEvent?.Invoke();
                break;
            case 3:
                PlayShopThemeEvent?.Invoke();
                break;
            case 4:
                PlayLevel3ThemeEvent?.Invoke();
                break;
            case 5:
                PlayShopThemeEvent?.Invoke();
                break;
            case 6:
                PlayBossLevelThemeEvent?.Invoke();
                break;
        }
    }
}
