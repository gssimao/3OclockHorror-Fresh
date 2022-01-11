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
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    private void Start()
    {
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
        myInv.CloseInv();
        myInv.InitStartingItems(Items);
    }



    public void BenchOpenClose()
    {
        if (uControls.Player.Interact.triggered && !active) // open the bench
        {
            IM.ActivateInventory(myInv);
            myInv.OpenInv(); //Update the items to be in accordance with the items array
            active = true;
            myInvDisplay.SetActive(true);
            invCanv.SetActive(true);
            IM.craftField.SetActive(true);
            tooltip.SetActive(false);
        }
        else if (uControls.Player.Interact.triggered && active) // close the bench
        {
            IM.DeactivateInventory(myInv);
            active = false;
            invCanv.SetActive(false);
            myInvDisplay.SetActive(false);
            IM.craftField.SetActive(false);
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Listener.BenchSwitch(true);
        OpenBench.TriggerBench += BenchOpenClose;

        
        //uControls.Player.Interact.performed += Inventory;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Listener.AdjustDrawer(Listener.GetInteractDrawer().transform.localPosition + new Vector3(0,-195,0), Vector3.Distance(player.transform.position, transform.position));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Listener.BenchSwitch(false);
        OpenBench.TriggerBench -= BenchOpenClose;
        
       /* Listener.GetInteractDrawer().transform.localPosition= Listener.GetOriginalDrawerPosition();*/
        //uControls.Player.Interact.performed -= Inventory;
    }
}
