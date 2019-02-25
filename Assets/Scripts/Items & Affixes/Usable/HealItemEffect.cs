using UnityEngine;

[CreateAssetMenu(menuName ="Item Effects/Healing Effect")]
public class HealItemEffect : UsableItemEffect
{
    public int healthAmount;

    public override void ExecuteEffect(UsableItem parentItem, CharacterManager character) {
        character.health += healthAmount;
    }

    public override string GetDescription() {
        return "Heals for " + healthAmount + " health.";
    }
}
