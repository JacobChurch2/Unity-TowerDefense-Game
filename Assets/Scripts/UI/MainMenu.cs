using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject startButton;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }
    private void OnEnable()
    {
        ShowPopup(eventSystem, startButton);
    }
    public void Begin() 
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ShowPopup(EventSystem eventSystem, GameObject newFirstSelected)
    {
        if (eventSystem != null && newFirstSelected != null)
        {
            eventSystem.SetSelectedGameObject(newFirstSelected);
        }
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.ExitPlaymode();
    }
    public void OpenOptions() 
    {
        this.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
