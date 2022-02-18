using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Item localItem;

    public static Action<Item, int> SendItem = delegate { };

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
            if(localItem.container.ID == 0)
                SendItem(localItem, 1); 
            else
                SendItem(localItem, 0); // send the item to the one who is listening
                                        //localItem.container.StopListening();//clicked slot stop listening to [ ItemSlot.SendItem ] 
                                        //ClearSlot(); // clear this slot since we finish sending the item (no longer needed)       
        }

    }
    public void Send()
    {
        if (localItem)
            SendItem(localItem, localItem.container.ID);
    }
    /*public void SendFromPuzzle(Item localItemFromPuzzle)
    {
        localItemFromPuzzle.container.StopListening();//clicked slot stop listening to [ ItemSlot.SendItem ] 
        SendItem(localItemFromPuzzle, localItem.container.ID); // send the item to the one who is listening
    }*/

    public virtual bool CanRecieveItem(Item item)
    {
        return true;
    }
}
