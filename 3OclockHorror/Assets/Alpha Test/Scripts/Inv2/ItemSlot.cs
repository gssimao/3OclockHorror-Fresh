using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    Image image;
    [SerializeField]
    Item localItem;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightClickEvent;
    public event Action<ItemSlot> onBeginDragEvent;
    public event Action<ItemSlot> onEndDragEvent;
    public event Action<ItemSlot> onDragEvent;
    public event Action<ItemSlot> onDropEvent;


    private Color normColor = Color.white;
    private Color disabledColor = new Color(255, 255, 255, 0.1f);

    public Item Item; //{ get; set; }
    public bool PlayerInv;

    public void Awake()
    {
        /*
        if (image == null)
        {
            image = GetComponent<Image>();        
        }*/
    }
    /*
    public void Update()
    {
        if(Item == null)
        {
            image.sprite = null;
            image.color = disabledColor;
        }
        else
        {
            image.sprite = Item.Icon;
            image.color = normColor;
        }
    }
    */
    public void UpdateSlot(Item value)
    {
        if (value == null) return;
        localItem = value;
        image.sprite = localItem.Icon;
        image.enabled = true;
    }
    public void ClearSlot()
    {
        image.sprite = null;
        image.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        if (image.sprite == null) return;
        //Remove Item from ContainerItem class
        localItem.container.RemoveItem(localItem);
        ClearSlot();
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClickEvent?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
        onPointerEnterEvent?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
        onPointerExitEvent?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        onDropEvent?.Invoke(this);
    }

    public virtual bool CanRecieveItem(Item item)
    {
        return true;
    }
}
