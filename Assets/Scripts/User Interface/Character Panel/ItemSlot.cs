using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Image image = null;
    [SerializeField] Text amountText;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightClickEvent;
    public event Action<ItemSlot> onBeginDragEvent;
    public event Action<ItemSlot> onEndDragEvent;
    public event Action<ItemSlot> onDragEvent;
    public event Action<ItemSlot> onDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item _item;
    public Item item {
        get {
            return _item;
        }
        
        set {
            _item = value;
            if (image != null) {
                if (_item == null) {
                    image.color = disabledColor;
                } else {
                    image.sprite = _item.icon;
                    image.color = normalColor;
                }
            }
        }
    }

    private int _amount;
    public int amount {
        get { return _amount; }
        set {
            _amount = value;
            amountText.enabled = _item != null && _item.maxStackSize > 1 && _amount > 1;
            if (amountText.enabled) {
                amountText.text = _amount.ToString();
            }
        }
    }

    protected virtual void OnValidate() {
        if (image == null) {
            image = GetComponent<Image>();
        }

        if (amountText == null) {
            amountText = GetComponentInChildren<Text>();
        }
    }

    public virtual bool CanReceiveItem(Item item) {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) {
            if (onRightClickEvent != null) {
                onRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (onPointerEnterEvent != null) {
            onPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (onPointerExitEvent != null) {
            onPointerExitEvent(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (onBeginDragEvent != null) {
            onBeginDragEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (onEndDragEvent != null) {
            onEndDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (onDragEvent != null) {
            onDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (onDropEvent != null) {
            onDropEvent(this);
        }
    }
}
