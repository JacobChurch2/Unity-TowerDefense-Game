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
        if (statTexts == null) return;

        for (int i = statTexts.Count - 1; i >= 0; i--)
        {
            if (statTexts[i] == null)
            {
                statTexts.RemoveAt(i);
                continue;
            }

            string statName = statTexts[i].gameObject.name.Split('_')[0];

            if (statManager != null)
            {
                float statValue = statManager.GetStat(statName);
                statTexts[i].text = $"{statValue}";
            }
        }
    }
    public void Unsubscribe()
    {
        statManager.OnStatChanged -= UpdateStatTexts;
    }
}
