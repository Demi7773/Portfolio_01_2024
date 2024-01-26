using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuScene;
    [SerializeField] private GameObject LevelSelectScreen;
    //[SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Button thisButton;
    [SerializeField] private Button nextButton;


    private void OnEnable()
    {
        PlayerEvents.GameOver += NotInteractible;
    }
    private void OnDisable()
    {
        PlayerEvents.GameOver -= NotInteractible;
    }
    private void NotInteractible()
    {
        thisButton.interactable = false;
    }


    public void ChooseThisLevel()
    {
        MainMenuScene.SetActive(false);
        LevelSelectScreen.SetActive(false);

        thisButton.interactable = false;
        nextButton.interactable = true;
    }
}
