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
        this.ShowPopup(eventSystem, startButton);
    }
    public void Begin() 
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // Only runs in the Unity Editor
#endif
    }
    public void OpenOptions() 
    {
        this.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
