using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveToggle : MonoBehaviour
{
    public int positionShow, positionHide;
    private void OnEnable()
    {
        ShowInteractionGA.ShowInteractButton += Show;
        HideInteractionGA.HideInteractButton += Hide;
    }
    private void OnDisable()
    {
        ShowInteractionGA.ShowInteractButton -= Show;
        HideInteractionGA.HideInteractButton -= Hide;
    }
    private void Show()
    {
        Debug.Log("Caaaaaaaaaaaaaa");
        LeanTween.moveLocalX(gameObject, positionShow, .5f).setEase(LeanTweenType.easeInQuad);
    }
    private void Hide()
    {
        Debug.Log("ooofffffff");
        LeanTween.moveLocalX(gameObject, positionHide, .5f).setEase(LeanTweenType.easeInQuad);
    }
}
