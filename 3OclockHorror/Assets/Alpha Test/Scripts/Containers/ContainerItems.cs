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
 
    private void OnDisable()
    {
        ItemSlot.SendItem -= ReceiveItem;
        ContainerUI.StopListening -= StopListening;
    }
    private void Awake()
    {
        //ID = getNewId();
        foreach (Item item in itemList)
        {
            if(item) //null check
                item.container = this; // Link Container with their items
        }
        ContainerUI.StopListening += StopListening;
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
        if (!v) return; //null check and return
        ContainerItems tempContainer = v.container;
        //Debug.Log(itemList.Count);
        for (int i = 0; i < itemList.Count; i++)
        {
            //if (bJournal)
                //Debug.Log("Journal countainer item receive item call");
            if(itemList[i] == null) // find a slot that is null
            {

                //Debug.Log("trying to get item " + v.name + " from " + v.container.transform.name);
                v.container = this;
                itemList[i] = v; // set that slot equal to the object
                tempContainer.RemoveItem(itemList[i]); // get that object container and remove this item from it
                tempContainer.Refresh();
                tempContainer.StartListening();                
                ShowUiSlotItems(itemList,ID);                
                break;
            }
        }
    }
}
