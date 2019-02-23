using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public Transform equipmentSlotsParent;
    public EquipmentSlot[] equipmentSlots;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightClickEvent;
    public event Action<ItemSlot> onBeginDragEvent;
    public event Action<ItemSlot> onEndDragEvent;
    public event Action<ItemSlot> onDragEvent;
    public event Action<ItemSlot> onDropEvent;

    private void Start() {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            equipmentSlots[i].onPointerEnterEvent += slot => onPointerEnterEvent(slot);
            equipmentSlots[i].onPointerExitEvent += slot => onPointerExitEvent(slot);
            equipmentSlots[i].onRightClickEvent += slot => onRightClickEvent(slot);
            equipmentSlots[i].onBeginDragEvent += slot => onBeginDragEvent(slot);
            equipmentSlots[i].onEndDragEvent += slot => onEndDragEvent(slot);
            equipmentSlots[i].onDragEvent += slot => onDragEvent(slot);
            equipmentSlots[i].onDropEvent += slot => onDropEvent(slot);
        }
    }

    private void OnValidate() {
        if (equipmentSlotsParent != null) {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }
    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem) {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].equipmentType == item.equipmentType) {
                previousItem = (EquippableItem)equipmentSlots[i].item;
                equipmentSlots[i].item = item;
                equipmentSlots[i].amount = 1;
                return true;
            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItem item) {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].item == item) {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        
        return false;
    }
}
