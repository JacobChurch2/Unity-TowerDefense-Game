using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObject")]
public class StatManager : ScriptableObject
{
    // Event to notify when a stat is updated
    public event System.Action OnStatChanged;

    private Dictionary<string, float> stats = new Dictionary<string, float>
    {
        { "TowerTotal", 0 },
        { "EnemySlainTotal", 0 },
        { "WavesDefeatedTotal", 0 },
        { "MoneySpentTotal", 0 }
    };

    private void OnEnable()
    {
        ResetStats();
        LoadStats();
    }

    public void AddToStat(string statName, float amount)
    {
        if (stats.ContainsKey(statName))
        {
            stats[statName] += amount;
            SaveStat(statName, stats[statName]);

            OnStatChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Stat '{statName}' not found.");
        }
    }

    public float GetStat(string statName)
    {
        return stats.ContainsKey(statName) ? stats[statName] : 0;
    }

    public void ResetStats()
    {
        foreach (var key in stats.Keys)
        {
            stats[key] = 0;
            PlayerPrefs.SetFloat(key, 0);
        }
        PlayerPrefs.Save();
    }

    private void SaveStat(string statName, float value)
    {
        PlayerPrefs.SetFloat(statName, value);
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        foreach (var key in new List<string>(stats.Keys)) // Create a copy of the keys
        {
            stats[key] = PlayerPrefs.GetFloat(key, 0);
        }
    }
}
