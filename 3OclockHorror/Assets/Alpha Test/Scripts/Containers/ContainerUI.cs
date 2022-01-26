using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public List<ItemSlot> itemSlots;

    private void OnEnable()
    {
        ContainerItems.ShowItems += ShowContainerUI;
        HideContainerItems.HideItems += HideContainerUI;
    }
    private void OnDisable()
    {
        ContainerItems.ShowItems -= ShowContainerUI;
        HideContainerItems.HideItems -= HideContainerUI;
    }
    private void ShowContainerUI(List<Item> items)
    {
        for(int x = 0;x < items.Count;x++)
        {
            if (x >= itemSlots.Count) break;
            itemSlots[x].UpdateSlot(items[x]);
        }
        LeanTween.moveLocalY(gameObject, 190, .5f).setEase(LeanTweenType.easeInQuad);        
    }
    private void HideContainerUI()
    {
        LeanTween.moveLocalY(gameObject, 430, .5f).setEase(LeanTweenType.easeInQuad);
        for (int x = 0; x < itemSlots.Count; x++)
            itemSlots[x].ClearSlot();
    }
}
