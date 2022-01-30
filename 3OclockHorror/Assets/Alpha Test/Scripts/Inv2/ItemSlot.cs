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
    public bool bItemReceived = false;

    public void UpdateSlot(Item value)
    {
        if (value == null)
            return;
        Debug.Log(transform.name);
        localItem = value;
        image.sprite = localItem.Icon;
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
        if (localItem && !bItemReceived)
        {
            ContainerItems.ItemReceived += ItemReceived;
            StartCoroutine(nameof(SendItemCrt));
        }

      /*  Debug.Log("Click");
        if (image.sprite == null) return;
        //Remove Item from ContainerItem class
        localItem.container.RemoveItem(localItem);
        ClearSlot();
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClickEvent?.Invoke(this);
        }*/
    }
    IEnumerator SendItemCrt()
    {
        localItem.container.StopListening();//clicked slot stop listening to [ ItemSlot.SendItem ] 
        SendItem(localItem); // send the item to the one who is listening
        bItemReceived = false;
        while(!bItemReceived)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("pass the coroutine");
        localItem.container.StartListening(); // make "this" start listening again
        ClearSlot(); // clear this slot since we finish sending the item
        ContainerItems.ItemReceived -= ItemReceived;
    }
    private void ItemReceived()
    {
        Debug.Log("passssssssss");
        bItemReceived = true;
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
