using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerUI : MonoBehaviour
{
    public bool bJournal;
    public int ID;
    public List<ItemSlot> itemSlots;
    public static Action StopListening = delegate { };

    private void OnEnable()
    {
        ContainerItems.ShowUiSlotItems += ShowContainerUI;
        HideContainerItems.HideItems += HideContainerUI;

    }
    private void OnDisable()
    {
        ContainerItems.ShowUiSlotItems -= ShowContainerUI;
        HideContainerItems.HideItems -= HideContainerUI;

    }

    private void ShowContainerUI(List<Item> ItemList,int ContainerId)
    {        
        if (ContainerId != ID) return;
        Debug.Log(ID + "  " + ContainerId);        
        for (int x = 0;x < ItemList.Count;x++)
        {
            if (x >= itemSlots.Count) 
                break;
            itemSlots[x].UpdateSlot(ItemList[x]);
        }
        if (bJournal) return;
        if (transform.localPosition.y == 190) return;
        LeanTween.moveLocalY(gameObject, 190, .5f).setEase(LeanTweenType.easeInQuad); // move journal down     
    }
    private void HideContainerUI()
    {
        StopListening();
        if (bJournal) return;
        LeanTween.moveLocalY(gameObject, 430, .5f).setEase(LeanTweenType.easeInQuad).setOnComplete(ClearSlotUiImage); // move journal up
    }
    private void ClearSlotUiImage()
    {
        for (int x = 0; x < itemSlots.Count; x++)
            itemSlots[x].ClearSlot();
    }
}
