using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private OptionsMenu optionsMenu;
    private Toggle fullscreenToggle;
    private Resolution[] resolutions;
    private List<string> resolutionOptions = new List<string>();
    void Start()
    {
        ResolutionDropDown();
        FullScreenToggle();
    }
    public void ResolutionDropDown() 
    {
        if (optionsMenu != null && optionsMenu.resolutionDropdown != null)
        {
            TMP_Dropdown resolutionDropdown = optionsMenu.resolutionDropdown;
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();
            int currentResolutionIndex = 0;

            // Populate dropdown with available resolutions
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                resolutionOptions.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(resolutionOptions);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            // Add listener for when the user changes resolution
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
    }
    public void FullScreenToggle() 
    {
        if (optionsMenu != null && optionsMenu.fullscreenToggle != null) 
        {
            fullscreenToggle = optionsMenu.fullscreenToggle;
            if (fullscreenToggle != null)
            {
                fullscreenToggle.isOn = Screen.fullScreen;
                fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            }
        }
    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
