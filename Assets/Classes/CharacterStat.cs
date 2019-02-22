using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public int id;
    // The base value of the stat.
    public float baseValue;

    // Hold old value to compare to new value, so we don't call for a calculation every time the value is needed.
    private float _value;

    // Return our stat value after modifiers have been applied.
    public float value {
        get {
            if (recalculate || baseValue != oldBaseValue) {
                oldBaseValue = baseValue;
                _value = CalculateFinalValue();
                recalculate = false;
            }
            return _value;
        }
    }

    // Bool to check if recalculation is required.
    private bool recalculate = true;

    private float oldBaseValue = float.MinValue;

    // Read-only list of the modifiers for the stat.
    public readonly List<StatModifier> statModifiers;

    // A read-only collection that allows us to display the modifiers for the player.
    private readonly ReadOnlyCollection<StatModifier> readableStatModifiers;

    public CharacterStat() {
        statModifiers = new List<StatModifier>();

        // Reflect changes in our other list into the readable list.
        readableStatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue, int id) : this() {
        this.baseValue = baseValue;
        this.id = id;
    }

    public void AddModifier(StatModifier mod) {
        // Since a modifier has been added, recalculate the value.
        recalculate = true;
        statModifiers.Add(mod);

        // Sort list into Flat > Percent order for proper calculation.
        statModifiers.Sort(CompareModifiersOrder);
    }

    // Comparison method.
    private int CompareModifiersOrder(StatModifier a, StatModifier b) {
        if (a.order < b.order) {
            return -1;
        } else if (a.order > b.order) {
            return 1;
        }

        // If they are equal.
        return 0;
    }

    public bool RemoveModifier(StatModifier mod) {
        // Since a modifier has been removed, recalculate the value.
        if(statModifiers.Remove(mod)) {
            recalculate = true;
            return true;
        }

        return false;
    }

    public bool RemoveAllModifiersFromSource(object source) {
        // Return true if a modifier was removed.
        bool didRemove = false;

        // Traverse the modifiers in reverse.
        for (int i = statModifiers.Count - 1; i >= 0; i--) {
            if (statModifiers[i].source == source) {
                recalculate = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    private float CalculateFinalValue() {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i  = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];

            if (mod.type == StatModifierType.Flat) {
                finalValue += mod.value;
            } else if (mod.type == StatModifierType.PercentAdd) {
                sumPercentAdd += mod.value;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModifierType.PercentAdd) {
                    finalValue *= 1 + sumPercentAdd;
                }
            } else if (mod.type == StatModifierType.PercentMulti) {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}