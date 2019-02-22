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
            itemSlots[i].onPointerEnterEvent += onPointerEnterEvent;
            itemSlots[i].onPointerExitEvent += onPointerExitEvent;
            itemSlots[i].onRightClickEvent += onRightClickEvent;
            itemSlots[i].onBeginDragEvent += onBeginDragEvent;
            itemSlots[i].onEndDragEvent += onEndDragEvent;
            itemSlots[i].onDragEvent += onDragEvent;
            itemSlots[i].onDropEvent += onDropEvent;
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
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++) {
            itemSlots[i].item = startingItems[i].GetCopy();
            itemSlots[i].amount = 1;
        }

        for (; i < itemSlots.Length; i++) {
            itemSlots[i].item = null;
            itemSlots[i].amount = 0;
        }
    }

    // If inventory is not full, add the item and return true.
    public bool AddItem(Item item) {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == null || (itemSlots[i].item.ID == item.ID && itemSlots[i].amount < item.maxStackSize)) {
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
                if (itemSlots[i].amount == 0) {
                    itemSlots[i].item = null;
                }
                return true;
            }
        }

        return false;
    }

    public bool IsFull() {
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].item == null) {
                return false;
            }
        }

        return true;
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
                amount++;
            }
        }

        return amount;
    }
}
