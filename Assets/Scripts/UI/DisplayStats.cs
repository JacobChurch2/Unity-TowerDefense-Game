using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats
{
    private StatManager statManager;
    private List<TMP_Text> statTexts;
    public DisplayStats(StatManager statManager, List<TMP_Text> statTexts)
    {
        this.statManager = statManager;
        this.statTexts = statTexts;
        
        // Subscribe to the OnStatChanged event
        this.statManager.OnStatChanged += UpdateStatTexts;
    }
    public void UpdateStatTexts()
    {
        foreach (TMP_Text stat in statTexts)
        {
            string statName = stat.gameObject.name.Split('_')[0];

            if (statManager != null)
            {
                float statValue = statManager.GetStat(statName);
                stat.text = $"{statValue}";
            }
        }
    }
    public void Unsubscribe()
    {
        statManager.OnStatChanged -= UpdateStatTexts;
    }
}
