using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenBenchGA : GameActions
{
    Inventory myInv;
    GameObject myInvDisplay;
    //[SerializeField] InventoryManager inventoryManager;

    private bool active; //Am I the active workbench?
    [SerializeField] List<Item> Items;
    GameObject tooltip;
    private GameObject invCanv;

    public static Action<Inventory> ActivateInventory = delegate{};
    public static Action<Inventory> DeactivateInventory = delegate { };
    public static Action<bool> CraftField = delegate { };

    private void OnEnable()
    {
        WorkbenchGO.UpdateWorkbenchDisplay += SetWorkbenchDisplay;
    }
    private void OnDisable()
    {
        WorkbenchGO.UpdateWorkbenchDisplay -= SetWorkbenchDisplay;
    }
    private void SetWorkbenchDisplay(GameObject disp,GameObject canvas, GameObject Tooltip)
    {
        myInvDisplay = disp;
        invCanv = canvas;
        tooltip = Tooltip;
    }
    public override void Action()
    {
        if (!active) // open the bench
        {
            active = true;
            if(myInv == null)
            {
                myInv = this.gameObject.GetComponent<Inventory>();
            }
            //inventoryManager.ActivateInventory(myInv);
            ActivateInventory(myInv);
            myInv.OpenInv(); //Update the items to be in accordance with the items array
            myInvDisplay.SetActive(true);
            invCanv.SetActive(true);
            //inventoryManager.craftField.SetActive(true);
            CraftField(true);
            tooltip.SetActive(false);
        }
        else// close the bench
        {
            active = false;
            if (myInv == null)
            {
                myInv = this.gameObject.GetComponent<Inventory>();
            }
            //inventoryManager.DeactivateInventory(myInv);
            DeactivateInventory(myInv);

            myInv.CloseInv(); // close inventory is getting a reference error
            myInvDisplay.SetActive(false);
            invCanv.SetActive(false);
            //inventoryManager.craftField.SetActive(false);
            CraftField(false);
        }
    }
}
