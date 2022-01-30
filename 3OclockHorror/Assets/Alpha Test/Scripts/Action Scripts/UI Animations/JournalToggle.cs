using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    public ContainerItems cItems;
    public int positionShow,positionHide;

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
        LeanTween.moveLocalX(gameObject, positionShow, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void Show(int position)
    {
        //cItems.StartListening();
        LeanTween.moveLocalX(gameObject, position, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void Hide()
    {
        cItems.StopListening();
        LeanTween.moveLocalX(gameObject, positionHide, .5f).setEase(LeanTweenType.easeInQuad);
    }
}
