using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames = null;

    private CharacterStat[] stats;

    // Get our components.
    private void Start()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    // Sets the stats we want to display. If there's not enough in the scene, return error.
    // If there's less than we want, disable them.
    public void SetStats(params CharacterStat[] charStats) {
        stats = charStats;

        if (stats.Length > statDisplays.Length) {
            Debug.LogError("Not enough displays for stats!");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++) {
            statDisplays[i].gameObject.SetActive(i < stats.Length);

            if (i < stats.Length) {
                statDisplays[i].stat = stats[i];
            }
        }
    }

    // Update our stat values.
    public void UpdateStatValues() {
        for (int i = 0; i < stats.Length; i++) {
            statDisplays[i].UpdateStatValue();
        }
    }

    // Update our stat names.
    public void UpdateStatNames() {
        for (int i = 0; i < stats.Length; i++) {
            statDisplays[i].statName = statNames[i];
        }
    }
}
