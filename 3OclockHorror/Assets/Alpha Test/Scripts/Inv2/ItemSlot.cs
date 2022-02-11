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
    public static Action<Item> SendItem = delegate { };

    public Item Item; //{ get; set; }
    public bool PlayerInv;
    public bool inventoryStatus;

    public void UpdateSlot(Item value)
    {
        if (value == null)
        {
            //this prevents the slot from having items when it supposed to be empty. This problem occurs when items are moved but
            //the UI isn't updated. Because the UI directly references Items, no clearing the slots would lead to errors
            localItem = null;
            image.sprite = null;
            image.color = new Color(0, 0, 0, 0); //removes white box when empty
            return;
        }
        //Debug.Log(transform.name);
        localItem = value;
        image.sprite = localItem.Icon;
        image.color = Color.white;
        image.enabled = true;
    }
    public void ClearSlot()
    {
        image.sprite = null;
        image.enabled = false;
        localItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryStatus)
            return;

        if (localItem)
        {
            localItem.container.StopListening();//clicked slot stop listening to [ ItemSlot.SendItem ] 
            SendItem(localItem); // send the item to the one who is listening
            //ClearSlot(); // clear this slot since we finish sending the item (no longer needed)       
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
