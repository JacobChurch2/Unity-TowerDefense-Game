using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        this.ShowPopup(eventSystem, backButton);  
    }

    public void BackToMenu() 
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
