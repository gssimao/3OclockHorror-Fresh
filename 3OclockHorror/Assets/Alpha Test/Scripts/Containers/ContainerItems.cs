using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerItems : GameActions
{
    public int ID;
    public List<Item> ContainerItemList;
    public static Action<List<Item>,int> ShowUiSlotItems = delegate { };
    public static Action<ContainerItems> ReceiveContainer = delegate { };
    public static Action<Item, int,int> SendItemToContainer = delegate { };

    public bool bJournal; 

    private void Awake()
    {
        PuzzleOpenerGA.ContainerRequest += SendContainer;        
        TableManager.SendItem += PuzzleReward;
        ContainerItems.SendItemToContainer += ContainerItemReceive;
        if(bJournal)
            ItemSlot.SendItem += ReceiveItem;
        foreach (Item item in ContainerItemList)
        {
            if(item) //null check
                item.container = this; // Link Container with their items
        }
    }
    private void OnDisable()
    {
        ItemSlot.SendItem -= ReceiveItem;
        PuzzleOpenerGA.ContainerRequest -= SendContainer;
        ContainerItems.SendItemToContainer -= ContainerItemReceive;
        TableManager.SendItem -= PuzzleReward;
    }   
    public void SendContainer(int containerID)
    {
        if (containerID == ID)
            ReceiveContainer(this);
    }
    public override void Action()
    {
        ShowUiSlotItems(ContainerItemList,ID);       
        ItemSlot.SendItem += ReceiveItem;
    }
    private void OnTriggerExit(Collider other)
    {
        ItemSlot.SendItem -= ReceiveItem;
    }
    public void RefreshUI()
    {
        ShowUiSlotItems(ContainerItemList, ID);
    }
    public void RemoveItem(Item value)
    {        
        for (int x = 0; x < ContainerItemList.Count; x++)
        {
            if (ContainerItemList[x] == value)
            {
                ContainerItemList[x] = null;
                break;
            }
        }
    }
    public bool ContainsItem(List<Item> Recepie)
    {

        bool check = false;
        List<Item> LocalStorage = new List<Item>();

        foreach(Item Localitem in ContainerItemList) // load items to the temp list
        {
            LocalStorage.Add(Localitem);
        }

        foreach (Item Recepieitem in Recepie) // check if container has all its necessery items
        {
            foreach (Item localItem in LocalStorage)
            {
                if(Recepieitem == localItem)
                {
                    LocalStorage.Remove(localItem);
                    check = true;
                    break;
                }
                check = false;
            }
            if (check == false)
                return check; // efficent way to return since one of the recepie items was not found in this counter
        }
        return check;

    }

    public void ReceiveItem(Item v, int incomingID)
    {
        if (incomingID == ID) return;
        if (!v) return; //null check and return
        
        ContainerItems tempContainer = v.container;
       
        for (int i = 0; i < ContainerItemList.Count; i++)
        {
            if(ContainerItemList[i] == null) // found a slot that is null
            {
                v.container = this;
                ContainerItemList[i] = v; // set that slot equal to the object
                tempContainer.RemoveItem(ContainerItemList[i]); // get that object container and remove this item from it
                tempContainer.RefreshUI();
                ShowUiSlotItems(ContainerItemList,ID);                
                break;
            }
        }
    }
    public void MendItem(Item v)
    {
        if (!v) return; //null check and return       
        ContainerItems tempContainer = v.container;
        for (int i = 0; i < ContainerItemList.Count; i++)
        {
            if (ContainerItemList[i] == null) // found a slot that is null
            {
                v.container = this;
                ContainerItemList[i] = v; // set that slot equal to the object

                if (tempContainer)
                {
                    tempContainer.RemoveItem(ContainerItemList[i]); // get that object container and remove this item from it
                    tempContainer.RefreshUI();
                }
                ShowUiSlotItems(ContainerItemList, ID);
                break;
            }
        }
    }

    //calls on containers with fromID to send their contents to containers with toID
    public void PuzzleReward(int fromID, int toID)
    {
        if (fromID == ID)
            SendItemToContainer(ContainerItemList[0], fromID, toID);
    }
    private void ContainerItemReceive(Item v, int fromID,int toID)
    {
        if(toID == ID)
            ReceiveItem(v, fromID);
    }
}
