using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour, IPopUp
{
    [SerializeField] private StatManager statManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private List<TMP_Text> statList;

    private DisplayStats display;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) // Check if the player presses the Escape key
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    private void OnEnable()
    {
        this.ShowPopup(eventSystem, quitButton);
        display = new DisplayStats(statManager, statList);
        display.UpdateStatTexts();
    }
  
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // Only runs in the Unity Editor
#endif
    }
}
