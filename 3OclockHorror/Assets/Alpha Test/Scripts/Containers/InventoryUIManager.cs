using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryGO;
    public GameObject containerBackground;
    private void OnEnable()
    {        
        GameContainerGA.ShowItems += Activate;
    }
    private void OnDisable()
    {
        GameContainerGA.ShowItems -= Activate;
    }
    private void Activate(List<Item> v)
    {
        Debug.Log("calling UI manager");
        inventoryGO.SetActive(true);
        containerBackground.SetActive(true);
    }
    private void Deactivate()
    {
        inventoryGO.SetActive(false);
        containerBackground.SetActive(false);
    }
}
