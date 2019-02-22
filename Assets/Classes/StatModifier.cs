using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public readonly float value;
    public readonly StatModifierType type;
    public readonly int order;

    // Object field to find the source of our modifiers.
    public readonly object source;

    public StatModifier(float value, StatModifierType type, int order, object source) {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    // Constructors below allow us to skip using order and source values when implementing a stat.
    public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }
    public StatModifier(float value, StatModifierType type, int order) : this(value, type, (int)type, order) { }
    public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }
}