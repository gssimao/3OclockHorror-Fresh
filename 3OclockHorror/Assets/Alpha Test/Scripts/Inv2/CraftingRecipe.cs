using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Inventory/Recipe", order = 2)]
public class CraftingRecipe : ScriptableObject
{
    public List<Item> Pieces;
    public Item result;
    public GameObject myButton;
    //public int minItems;
    //public bool notPuzzle;

    public bool CanCraft(ContainerItems container) // check if all items needed are available to craft
    {
        
        bool craft = false;
        craft = container.ContainsItem(Pieces);// checking all slots of the container for the item returns true if found
        Debug.Log("Can I craft? " + craft);
        return craft;

    }

    public void Craft(ContainerItems container)
    {
        if (!CanCraft(container))
            return;
        //check all items in ContainerItems script
        //remove items from container
        //send item to container
        foreach (Item item in Pieces)
        {
            container.RemoveItem(item);
        }
        container.MendItem(result);


        //int itemsRemoved = 0;
        /*
         List<Item> removed = new List<Item>();
        
        if (itemsRemoved != minItems) // checking if we removed all of the neceserry items and if not >> put them back
        {
            foreach (Item itm in removed)
            {
                //need a way to add items
                container.ContainerItemList.Add(itm);
            }
        }*/

        /*else // check went through! add the result to the slot
        {
            //need a way to add items
           
            //container.ContainerItemList.(result);
        }*/


    }
}
