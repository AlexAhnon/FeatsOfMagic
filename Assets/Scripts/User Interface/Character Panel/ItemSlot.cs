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

    protected bool isPointerOver;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item _item;
    public Item item {
        get { return _item; }
        
        set {
            _item = value;
            if (_item == null && amount != 0) amount = 0;

            if (_item == null) {
                image.color = disabledColor;
            } else {
                image.sprite = _item.icon;
                image.color = normalColor;
            }

            if (isPointerOver) {
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        }
    }

    private int _amount;
    public int amount {
        get { return _amount; }
        set {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && item != null) item = null;

            if (amountText != null) {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled) {
                    amountText.text = _amount.ToString();
                }
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

    protected virtual void OnDisable() {
        if (isPointerOver) {
            OnPointerExit(null);
        }
    }

    public virtual bool CanAddStack(Item item, int amount = 1) {
        return this.item != null && this.item.ID == item.ID && this.amount + amount <= item.maxStackSize;
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
        isPointerOver = true;

        if (onPointerEnterEvent != null) {
            onPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerOver = false;

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
