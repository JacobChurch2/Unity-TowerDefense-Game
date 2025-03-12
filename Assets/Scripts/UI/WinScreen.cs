using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour, IPopUp
{
    [SerializeField] private List<GameObject> disableScreens;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private EventSystem eventSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.ShowPopup(eventSystem, quitButton);
        DisableScreens(disableScreens);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // Only runs in the Unity Editor
#endif
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    private void DisableScreens(List<GameObject> screens)
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }
}