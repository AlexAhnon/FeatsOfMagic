using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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
}
