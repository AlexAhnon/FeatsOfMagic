using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Item Effects/Stat Buff Effect")]
public class StatBuffItemEffect : UsableItemEffect
{
    public int statId;
    public int buffAmount;
    public float duration;

    public override void ExecuteEffect(UsableItem parentItem, CharacterManager character) {
        StatModifier modifier = new StatModifier(buffAmount, StatModifierType.Flat, parentItem);
        bool isAdded = false;

        foreach (CharacterStat stat in character.stats) {
            if (stat.id == statId) {
                stat.AddModifier(modifier);
                isAdded = true;
            }
        }

        if (isAdded) {
            character.StartCoroutine(RemoveBuff(character, modifier, duration));
            character.UpdateStatValues();
        }
    }

    public override string GetDescription() {
        return "Grants " + buffAmount + "buff for " + duration + " seconds.";
    }

    private IEnumerator RemoveBuff(CharacterManager character, StatModifier modifier, float duration) {
        yield return new WaitForSeconds(duration);

        foreach (CharacterStat stat in character.stats) {
            if (stat.id == statId) {
                stat.RemoveModifier(modifier);
                character.UpdateStatValues();
            }
        }
    }
}
