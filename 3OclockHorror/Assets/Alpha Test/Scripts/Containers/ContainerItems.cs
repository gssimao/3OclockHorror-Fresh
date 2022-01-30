using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerItems : GameActions
{
    public int ID;
    public List<Item> itemList;
    public static Action<List<Item>,int> ShowUiSlotItems = delegate { };
    public bool bJournal;
    public static Action ItemReceived = delegate { };

    private void OnDisable()
    {
        ItemSlot.SendItem -= ReceiveItem;
    }
    private void Awake()
    {
        foreach (Item item in itemList)
        {
            item.container = this; // Link Container with their items
        }
            
    }
    public override void Action()
    {
        ShowUiSlotItems(itemList,ID);
        ItemSlot.SendItem += ReceiveItem;
    }
    public void Refresh()
    {
        ShowUiSlotItems(itemList, ID);
    }
    public void StopListening()
    {
        ItemSlot.SendItem -= ReceiveItem;
    }
    public void StartListening()
    {
        ItemSlot.SendItem += ReceiveItem;
    }
    public void RemoveItem(Item value)
    {
        for (int x = 0; x < itemList.Count; x++)
        {
            if (itemList[x] == value)
            {
                itemList[x] = null;
                break;
            }
        }
    }
    private void ReceiveItem(Item v)
    {
        Debug.Log(itemList.Count);
        for (int i = 0; i < itemList.Count; i++)
        {
            if (bJournal)
                Debug.Log("Journal countainer item receive item call");
            if(itemList[i] == null) // find a slot that is null
            {

                Debug.Log("trying to get item " + v.name + " from " + v.container.transform.name);
                itemList[i] = v; // set that slot equal to the object
                itemList[i].container.RemoveItem(itemList[i]); // get that object container and remove this item from it
                itemList[i].container.Refresh();
                itemList[i].container = this;
                ShowUiSlotItems(itemList,ID);
                ItemReceived();
                break;
            }
        }
        ItemReceived();
    }
}
