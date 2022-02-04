using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    public ContainerItems cItems;
    public int positionShow,positionHide;
    public List<ItemSlot> itemSlots;

    private void OnEnable()
    {
        ShowJournal.Show += Show;
        FullyOpenJournal.FullShow += Show;
        HideJournal.Hide += Hide;
    }
    private void OnDisable()
    {
        ShowJournal.Show -= Show;
        HideJournal.Hide -= Hide;
    }
    private void Show()
    {
        cItems.StartListening();
        cItems.Refresh();
        LeanTween.moveLocalX(gameObject, positionShow, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void Show(int position)
    {
        TurnoffButton();
        LeanTween.moveLocalX(gameObject, position, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void Hide()
    {
        TurnOnButton();
        cItems.StopListening();
        LeanTween.moveLocalX(gameObject, positionHide, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void TurnoffButton()
    {
        for(int i =0; i<itemSlots.Count; i++)
        {
            itemSlots[i].inventoryStatus = true;
        }
    }
    private void TurnOnButton()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].inventoryStatus = false; 
        }
    }
}
