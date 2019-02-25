using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Usable Item")]
public class UsableItem : Item
{
    public bool isConsumable;

    public List<UsableItemEffect> effects;

    public virtual void Use(CharacterManager character) {
        foreach(UsableItemEffect effect in effects) {
            effect.ExecuteEffect(this, character);
        }
    }

    public override string GetItemType() {
        return base.GetItemType();
    }

    public override string GetDescription() {
        sb.Length = 0;

        foreach (UsableItemEffect effect in effects) {
            sb.AppendLine(effect.GetDescription());
        }

        return sb.ToString();
    }
}
