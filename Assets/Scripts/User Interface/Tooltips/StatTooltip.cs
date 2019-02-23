using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] Text statNameText = null;
    [SerializeField] Text statModifierLabelText = null;
    [SerializeField] Text statModifiersText = null;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName) {
        statNameText.text = GetStatTopText(stat, statName);

        statModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }

    public void HideTooltip() {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName) {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.value);

        if (stat.value != stat.baseValue) {
            sb.Append(" (");
            sb.Append(stat.baseValue);

            if (stat.value > stat.baseValue) {
                sb.Append("+");
            }

            sb.Append(System.Math.Round(stat.value - stat.baseValue, 4));
            sb.Append(")");
        }

        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat) {
        sb.Length = 0;

        foreach (StatModifier mod in stat.statModifiers) {
            if (sb.Length > 0) {
                sb.AppendLine();
            }

            if (mod.value > 0) {
                sb.Append("+");
            }

            if (mod.type == StatModifierType.Flat) {
                sb.Append(mod.value);
            } else {
                sb.Append(mod.value * 100);
                sb.Append("%");
            }

            Item item = mod.source as Item;

            if (item != null) {
                sb.Append(" ");
                sb.Append(item.displayName);
            } else {
                Debug.LogError("Modifier is not an Item!");
            }
        }

        return sb.ToString();
    }
}
