using UnityEngine;

[CreateAssetMenu(menuName ="Effect/Healing Item")]
public class HealItemEffect : UsableItemEffect
{
    public int healthAmount;

    public override void ExecuteEffect(UsableItem parentItem, Character character) {
        character.health += healthAmount;
    }
}
