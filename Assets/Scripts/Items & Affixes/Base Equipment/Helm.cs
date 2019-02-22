using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Base Equipment/Helm")]
public class Helm : EquippableItem
{
    public int defense;

    public override void Equip(Character c) {
        base.Equip(c);

        for (int i = 0; i < c.stats.Count; i++) {
            if (c.stats[i].id == 4) {
                c.stats[i].baseValue += defense;
            }
        }
    }

    public override void Unequip(Character c) {
        base.Unequip(c);

        for (int i = 0; i < c.stats.Count; i++) {
            if (c.stats[i].id == 4) {
                c.stats[i].baseValue -= defense;
            }
        }
    }
}
