using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Equippable Item")]
public class EquippableItem : Item
{
    [Space]
    public List<ItemAffix> affixes;
    [Space]
    public EquipmentType equipmentType;
    public ItemRarity rarity;

    public virtual void Verify() {
        if (affixes == null)
            affixes = new List<ItemAffix>();
    }

    public override Item GetCopy() {
        return Instantiate(this);
    }

    public override void Destroy() {
        Destroy(this);
    }

    public virtual void Equip(Character c) {
        foreach (ItemAffix affix in affixes) {
            for (int i = 0; i < c.stats.Count; i++) {
                if (c.stats[i].id == affix.id) {
                    c.stats[i].AddModifier(new StatModifier(affix.value, affix.modifierType, this));
                }
            }
        }
    }

    public virtual void Unequip(Character c) {
        foreach (ItemAffix affix in affixes) {
            for (int i = 0; i < c.stats.Count; i++) {
                if (c.stats[i].id == affix.id) {
                    c.stats[i].RemoveAllModifiersFromSource(this);
                }
            }
        }
    }

    public override string GetItemType() {
        return equipmentType.ToString();
    }

    public override string GetDescription() {
        sb.Length = 0;

        foreach (ItemAffix affix in affixes) {
            if (affix.modifierType == StatModifierType.Flat) {
                AddStat(affix.value, affix.displayName);
            } else if (affix.modifierType == StatModifierType.PercentMulti) {
                AddStat(affix.value, affix.displayName, isPercent: true);
            }
        }

        return sb.ToString();
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
