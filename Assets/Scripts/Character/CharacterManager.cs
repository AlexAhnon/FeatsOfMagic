using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    // Character stats.
    [SerializeField]
    public List<CharacterStat> stats;

    public int health = 50;

    public Inventory inventory;
    public EquipmentPanel equipmentPanel;
    public StatPanel statPanel;
    public ItemTooltip itemTooltip;
    
    public Image draggableItem;
    private ItemSlot dragItemSlot;

    private void OnValidate() {
        if (itemTooltip == null) {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake() {
        // Set the stats that we want in our stat panel.
        statPanel.SetStats(stats.ToArray());
        UpdateStatValues();

        // Initialize event handlers.
        // Right-click
        inventory.onRightClickEvent += InventoryRightClick;
        equipmentPanel.onRightClickEvent += EquipmentPanelRightClick;

        // Pointer enter
        inventory.onPointerEnterEvent += ShowTooltip;
        equipmentPanel.onPointerEnterEvent += ShowTooltip;

        // Pointer exit
        inventory.onPointerExitEvent += HideTooltip;
        equipmentPanel.onPointerExitEvent += HideTooltip;

        // Begin drag
        inventory.onBeginDragEvent += BeginDrag;
        equipmentPanel.onBeginDragEvent += BeginDrag;

        // End drag
        inventory.onEndDragEvent += EndDrag;
        equipmentPanel.onEndDragEvent += EndDrag;

        // Drag
        inventory.onDragEvent += Drag;
        equipmentPanel.onDragEvent += Drag;

        // Drop
        inventory.onDropEvent += Drop;
        equipmentPanel.onDropEvent += Drop;
    }

    private void InventoryRightClick(ItemSlot itemSlot) {
        if (itemSlot.item is EquippableItem) {
            Equip((EquippableItem)itemSlot.item);
        } else if (itemSlot.item is UsableItem) {
            UsableItem usableItem = (UsableItem)itemSlot.item;
            usableItem.Use(this);

            if (usableItem.isConsumable) {
                inventory.RemoveItemFromSlot(usableItem, itemSlot);
                usableItem.Destroy();
            }
        }
    }

    private void EquipmentPanelRightClick(ItemSlot itemSlot) {
        if (itemSlot.item is EquippableItem) {
            Unequip((EquippableItem)itemSlot.item);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot) {
        if (itemSlot.item != null) {
            itemTooltip.ShowTooltip(itemSlot.item);
        }
    }

    private void HideTooltip(ItemSlot itemSlot) {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot) {
        if (itemSlot.item != null) {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot) {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot) {
        draggableItem.transform.position = Input.mousePosition;
    }

    private void AddStacks(ItemSlot itemSlot) {
        int numAddablestacks = itemSlot.item.maxStackSize - itemSlot.amount;
        int stacksToAdd = Mathf.Min(numAddablestacks, dragItemSlot.amount);

        itemSlot.amount += stacksToAdd;
        dragItemSlot.amount -= stacksToAdd;
    }

    private void SwapItems(ItemSlot itemSlot) {
        EquippableItem dragItem = dragItemSlot.item as EquippableItem;
        EquippableItem dropItem = itemSlot.item as EquippableItem;

        if (itemSlot is EquipmentSlot) {
            if (dragItem != null) {
                dragItem.Equip(this);
            }

            if (dropItem != null) {
                dropItem.Unequip(this);
            }
        }

        if (dragItemSlot is EquipmentSlot) {
            if (dragItem != null) {
                dragItem.Unequip(this);
            }

            if (dropItem != null) {
                dropItem.Equip(this);
            }
        }

        UpdateStatValues();

        Item draggedItem = dragItemSlot.item;
        int draggedItemAmount = dragItemSlot.amount;

        dragItemSlot.item = itemSlot.item;
        dragItemSlot.amount = itemSlot.amount;
                    
        itemSlot.item = draggedItem;
        itemSlot.amount = draggedItemAmount;
    }

    private void Drop(ItemSlot dropItemSlot) {
        if (dragItemSlot == null) {
            return;
        }

        if (dropItemSlot.CanAddStack(dragItemSlot.item)) {
            AddStacks(dropItemSlot);
        } else if (dropItemSlot.CanReceiveItem(dragItemSlot.item) && dragItemSlot.CanReceiveItem(dropItemSlot.item)) {
            SwapItems(dropItemSlot);
        }
    }

    // Equip an item from the inventory, checks if it's equippable.
    public void Equip(EquippableItem item) {
        if (inventory.RemoveItem(item)) {
            // Keep a reference to the old item if the slot is occupied so we can return it to our inventory.
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem)) {
                if (previousItem != null) {
                    inventory.AddItem(previousItem);
                    // If there is an item in our equipment slot, unequip it and return it to inventory.
                    previousItem.Unequip(this);
                    UpdateStatValues();
                }
                // Equip our new item to our slot.
                item.Equip(this);
                UpdateStatValues();
            } else {
                inventory.AddItem(item);
            }
        }
    }

    // Unequip an item, only if our inventory isn't full.
    public void Unequip(EquippableItem item) {
        if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item)) {
            inventory.AddItem(item);
            item.Unequip(this);
            UpdateStatValues();
        }
    }

    public void UpdateStatValues() {
        statPanel.UpdateStatValues();
    }
}
