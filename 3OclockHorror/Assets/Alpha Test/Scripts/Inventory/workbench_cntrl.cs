using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
public class workbench_cntrl : MonoBehaviour
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
    public invInput Listener;
    public GameObject invCanv;


    UniversalControls uControls;
    private void Awake()
    {
        Listener = GameObject.Find("Listener").GetComponent<invInput>();
        uControls = new UniversalControls();
        uControls.Enable();

        //ItemPopups = GameObject.FindGameObjectsWithTag("ItemPopup");
        if (myInv == null)
        {
            myInv = gameObject.GetComponent<Inventory>();
        }

        if (invCanv == null)
        {
            invCanv = GameObject.FindGameObjectWithTag("invUI");
        }

        active = false;
        //myInv.CloseInv();
        myInv.InitStartingItems(Items);
    }
    private void OnDisable()
    {
        uControls.Disable();
    }

    public void BenchOpenClose()
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
