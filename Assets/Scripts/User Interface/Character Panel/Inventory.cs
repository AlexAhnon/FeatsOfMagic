using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IItemContainer
{
    [SerializeField] List<Item> startingItems = null;
    [SerializeField] Transform itemsParent = null;
    [SerializeField] ItemSlot[] itemSlots = null;
    public ItemGenerator itemGenerator;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightClickEvent;
    public event Action<ItemSlot> onBeginDragEvent;
    public event Action<ItemSlot> onEndDragEvent;
    public event Action<ItemSlot> onDragEvent;
    public event Action<ItemSlot> onDropEvent;

    // Add handlers for all of our item slots.
    private void Start() {
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].onPointerEnterEvent += slot => onPointerEnterEvent(slot);
            itemSlots[i].onPointerExitEvent += slot => onPointerExitEvent(slot);
            itemSlots[i].onRightClickEvent += slot => onRightClickEvent(slot);
            itemSlots[i].onBeginDragEvent += slot => onBeginDragEvent(slot);
            itemSlots[i].onEndDragEvent += slot => onEndDragEvent(slot);
            itemSlots[i].onDragEvent += slot => onDragEvent(slot);
            itemSlots[i].onDropEvent += slot => onDropEvent(slot);
        }

        if (startingItems != null) {
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            startingItems.Add(itemGenerator.GenerateRandomEquippableItem());
            SetStartingItems();
        }
    }

    // Get the components.
    private void OnValidate()
    {
        if (itemsParent != null) {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }

    // A call to refresh the UI when we want to.
    private void SetStartingItems() {
        Clear();

        for (int i = 0; i < startingItems.Count; i++) {
            AddItem(startingItems[i].GetCopy());
        }
    }

    public virtual bool CanAddItem(Item item, int amount = 1) {
        int freeSpaces = 0;

        foreach (ItemSlot itemSlot in itemSlots) {
            if (itemSlot.item == null || itemSlot.item.ID == item.ID) {
                freeSpaces += item.maxStackSize - itemSlot.amount;
            }
        }

        return freeSpaces >= amount;
    }

    // If inventory is not full, add the item and return true.
    public bool AddItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].CanAddStack(item)) {
                itemSlots[i].item = item;
                itemSlots[i].amount++;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == null) {
                itemSlots[i].item = item;
                itemSlots[i].amount++;
                return true;
            }
        }

        return false;
    }

    // Remove an item from the inventory.
    public bool RemoveItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == item) {
                itemSlots[i].amount--;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItemFromSlot(Item item, ItemSlot itemSlot) {
        if (itemSlot.item == item) {
            itemSlot.amount--;
            return true;
        }

        return false;
    }

    public bool ContainsItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == item) {
                return true;
            }
        }

        return false;
    }

    public int ItemCount(Item item) {
        int amount = 0;
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == item) {
                amount += itemSlots[i].amount;
            }
        }

        return amount;
    }

    public virtual void Clear() {
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].item = null;
        }
    }
}
