using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipieHolder : MonoBehaviour
{

    [SerializeField]
    List<CraftingRecipe> Recipies;

    public void Craft()
    {
        
        foreach(CraftingRecipe recipe in Recipies)
        {           
            recipe.Craft(this.GetComponent<ContainerItems>());
        }
    }
}
