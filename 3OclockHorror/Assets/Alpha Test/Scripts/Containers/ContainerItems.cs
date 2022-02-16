using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerItems : GameActions
{
    public int ID;
    public List<Item> ContainerItemList;
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
        foreach (Item item in ContainerItemList)
        {
            if(item) //null check
                item.container = this; // Link Container with their items
        }
        ContainerUI.StopListening += StopListening;
    }
    public override void Action()
    {
        ShowUiSlotItems(ContainerItemList,ID);
        ItemSlot.SendItem += ReceiveItem;
    }
    public void RefreshUI()
    {
        ShowUiSlotItems(ContainerItemList, ID);
    }
    public void StopListening()
    {
        ItemSlot.SendItem -= ReceiveItem;
    }
    public void StartListening()
    {
        Debug.Log("start listening");
        ItemSlot.SendItem += ReceiveItem;
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

    public void ReceiveItem(Item v)
    {

        if (!v) return; //null check and return
        
        ContainerItems tempContainer = v.container;
        //Debug.Log(itemList.Count);
        for (int i = 0; i < ContainerItemList.Count; i++)
        {
            if(ContainerItemList[i] == null) // found a slot that is null
            {

                //Debug.Log("trying to get item " + v.name + " from " + v.container.transform.name);
                v.container = this;
                ContainerItemList[i] = v; // set that slot equal to the object
                tempContainer.RemoveItem(ContainerItemList[i]); // get that object container and remove this item from it
                tempContainer.RefreshUI();
                tempContainer.StartListening();
                ShowUiSlotItems(ContainerItemList,ID);                
                break;
            }
        }
    }
    public void MendItem(Item v)
    {
        if (!v) return; //null check and return       
        ContainerItems tempContainer = v.container;
        //Debug.Log(itemList.Count);
        for (int i = 0; i < ContainerItemList.Count; i++)
        {
            if (ContainerItemList[i] == null) // found a slot that is null
            {

                //Debug.Log("trying to get item " + v.name + " from " + v.container.transform.name);
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
}
