using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieHolder : MonoBehaviour
{
    [SerializeField] ContainerItems containerItems;
    [SerializeField] List<CraftingRecipe> Recipies;

    private void OnEnable()
    {
        CallCraftDelegate.Craft += Craft;

    }
    private void OnDisable()
    {
        CallCraftDelegate.Craft -= Craft;

    }

    public void Craft()
    {
        
        foreach(CraftingRecipe recipe in Recipies)
        {           
            recipe.Craft(containerItems);
        }
    }
}
