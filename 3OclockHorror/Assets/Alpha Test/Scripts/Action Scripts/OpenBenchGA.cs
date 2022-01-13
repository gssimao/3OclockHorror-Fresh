using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBenchGA : GameActions
{
    [SerializeField]
    Inventory myInv;
    [SerializeField]
    GameObject myInvDisplay;
    [SerializeField]
    InventoryManager IM;

    bool active; //Am I the active workbench?
    [SerializeField]
    List<Item> Items;
    [SerializeField]
    GameObject tooltip;
    public GameObject invCanv;

    public override void Action()
    {
        if (!active) // open the bench
        {
            IM.ActivateInventory(myInv);
            myInv.OpenInv(); //Update the items to be in accordance with the items array
            active = true;
            myInvDisplay.SetActive(true);
            invCanv.SetActive(true);
            IM.craftField.SetActive(true);
            tooltip.SetActive(false);
        }
        else// close the bench
        {
            IM.DeactivateInventory(myInv);
            active = false;
            invCanv.SetActive(false);
            myInvDisplay.SetActive(false);
            IM.craftField.SetActive(false);
        }
    }
}
