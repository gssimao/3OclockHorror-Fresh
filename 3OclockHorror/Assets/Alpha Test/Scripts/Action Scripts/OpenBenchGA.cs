using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBenchGA : GameActions
{
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
            active = true;
            if(myInv == null)
            {
                myInv = this.gameObject.GetComponent<Inventory>();
            }
            IM.ActivateInventory(myInv);
            myInv.OpenInv(); //Update the items to be in accordance with the items array
            myInvDisplay.SetActive(true);
            invCanv.SetActive(true);
            IM.craftField.SetActive(true);
            tooltip.SetActive(false);
        }
        else// close the bench
        {
            active = false;
            if (myInv == null)
            {
                myInv = this.gameObject.GetComponent<Inventory>();
            }
            IM.DeactivateInventory(myInv);
            myInv.CloseInv(); // close inventory is getting a reference error
            myInvDisplay.SetActive(false);
            invCanv.SetActive(false);
            IM.craftField.SetActive(false);
        }
    }
}
