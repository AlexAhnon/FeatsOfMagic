using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="Items/Item")]
public class Item : ScriptableObject
{
    public string displayName;
    public string ID;
    
    [Range(1, 999)]
    public int maxStackSize = 1;

    public Sprite icon;

    public ItemType type;

    private void OnValidate() {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetCopy() {
        return this;
    }

    public virtual void Destroy() {

    }
}