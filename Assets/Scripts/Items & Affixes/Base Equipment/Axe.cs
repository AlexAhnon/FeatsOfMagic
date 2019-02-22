using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Base Equipment/Axe")]
public class Axe : EquippableItem
{
    public int damage;
    public float attackSpeed;
    
    public override void Equip(Character c) {
        base.Equip(c);

        for (int i = 0; i < c.stats.Count; i++) {
            if (c.stats[i].id == 5) {
                c.stats[i].baseValue += damage;
            } else if (c.stats[i].id == 6) {
                c.stats[i].baseValue = attackSpeed;
            }
        }
    }

    public override void Unequip(Character c) {
        base.Unequip(c);

        for (int i = 0; i < c.stats.Count; i++) {
            if (c.stats[i].id == 5) {
                c.stats[i].baseValue -= damage;
            } else if (c.stats[i].id == 6) {
                c.stats[i].baseValue = 1;
            }
        }
    }
}
