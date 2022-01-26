using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerItems : GameActions
{
    public List<Item> itemList;
    public static Action<List<Item>> ShowItems = delegate { };

    private void Awake()
    {
        foreach (Item item in itemList)
            item.container = this;
    }
    public override void Action()
    {
        ShowItems(itemList);
    }
    public void RemoveItem(Item value)
    {
        for (int x = 0; x < itemList.Count; x++)
        {
            if (itemList[x] == value)
            {
                itemList.RemoveAt(x);
                break;
            }
        }
    }
}
