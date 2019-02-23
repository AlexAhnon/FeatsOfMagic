using System;
using System.Text;
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

    protected static readonly StringBuilder sb = new StringBuilder();

    private void OnValidate() {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetCopy() {
        return this;
    }

    public virtual void Destroy() {

    }

    public virtual string GetItemType() {
        return type.ToString();
    }

    public virtual string GetDescription() {
        return "";
    }
}