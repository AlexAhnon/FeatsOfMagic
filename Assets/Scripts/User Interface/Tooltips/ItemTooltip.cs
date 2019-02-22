using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text itemNameText = null;
    [SerializeField] Text itemClassText = null;
    [SerializeField] Text itemDescriptionText = null;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(EquippableItem item) {
        itemNameText.text = item.displayName;
        itemClassText.text = item.equipmentType.ToString();

        sb.Length = 0;

        foreach (ItemAffix affix in item.affixes) {
            if (affix.modifierType == StatModifierType.Flat) {
                AddStat(affix.value, affix.displayName);
            } else if (affix.modifierType == StatModifierType.PercentMulti) {
                AddStat(affix.value, affix.displayName, isPercent: true);
            }
        }

        itemDescriptionText.text = sb.ToString();

        switch(item.rarity) {
            case ItemRarity.Normal:
                this.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                break;
            case ItemRarity.Magic:
                this.GetComponent<Image>().color = new Color32(0, 106, 255, 100);
                break;
            case ItemRarity.Rare:
                this.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case ItemRarity.Legendary:
                this.GetComponent<Image>().color = new Color32(255, 130, 0, 100);
                break;
        }

        gameObject.SetActive(true);
    }

    public void HideTooltip() {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false) {
        if (value != 0) {
            if (sb.Length > 0) {
                sb.AppendLine();
            }

            if (value > 0) {
                sb.Append("+");
            }

            if (isPercent) {
                sb.Append(value * 100);
                sb.Append("% ");
            } else {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
