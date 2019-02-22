using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    public virtual void Verify() {
        if (items == null)
            items = new List<Item>();
    }
}
