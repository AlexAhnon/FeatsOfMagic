using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Affixes/Item Affix")]
public class ItemAffix : ScriptableObject {
    public int id;
    public string displayName;
    public float minValue;
    public float maxValue;
    public float value;
    public int levelRequirement;
    public StatModifierType modifierType;
}
