using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI; // If using TextMeshPro

public class OptionsMenu : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] public TMP_Dropdown qualityDropDown;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    [SerializeField] public Toggle fullscreenToggle;
    [SerializeField] public Toggle endlessModeToggle;

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject backButton;

    private void OnEnable()
    {
        ShowPopup(eventSystem, backButton);
    }

    public void ShowPopup(EventSystem eventSystem, GameObject newFirstSelected)
    {
        if (eventSystem != null && newFirstSelected != null)
        {
            eventSystem.SetSelectedGameObject(newFirstSelected);
        }
    }

    public void BackToMenu() 
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
