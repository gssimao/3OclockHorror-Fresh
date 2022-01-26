using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameContainerGA : GameActions
{
    public List<Item> ItemHolder;
    public static Action<List<Item>> ShowItems = delegate { }; 
    public override void Action()
    {
        ShowItems(ItemHolder);
    }
}
