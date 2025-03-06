using TMPro;
using UnityEngine;

public class QualityManager : MonoBehaviour
{
    [SerializeField] private OptionsMenu optionsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualityDropDown();
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }
    private void QualityDropDown() 
    {
        if (optionsMenu != null && optionsMenu.qualityDropDown != null)
        {
            TMP_Dropdown qualityDropDown = optionsMenu.qualityDropDown;

            // Populate dropdown with quality settings
            qualityDropDown.ClearOptions();
            qualityDropDown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));

            // Set the dropdown to reflect the current quality setting
            qualityDropDown.value = QualitySettings.GetQualityLevel();
            qualityDropDown.RefreshShownValue();

            // Add listener to notify QualityManager when changed
            qualityDropDown.onValueChanged.AddListener(SetQuality);
        }
    }
}
