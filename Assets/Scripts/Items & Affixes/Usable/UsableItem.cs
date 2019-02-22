using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Usable Item")]
public class UsableItem : Item
{
    public bool isConsumable;

    public List<UsableItemEffect> effects;

    public virtual void Use(Character character) {
        foreach(UsableItemEffect effect in effects) {
            effect.ExecuteEffect(this, character);
        }
    }
}
