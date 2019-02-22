using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Affixes/Affix Database")]
public class AffixDatabase : ScriptableObject
{
    public List<ItemAffix> affixes;

    public virtual void Verify() {
        if (affixes == null)
            affixes = new List<ItemAffix>();
    }
}
