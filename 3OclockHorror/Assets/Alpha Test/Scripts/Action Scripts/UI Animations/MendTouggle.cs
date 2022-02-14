using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MendTouggle : MonoBehaviour
{
    public int positionShow, positionHide;
    public GameObject MendObject;
    private void OnEnable()
    {
        ShowMend.MendShow += showMendUI;
        HideMend.MendHide += HideMendUI;
    }
    private void OnDisable()
    {
        ShowMend.MendShow -= showMendUI;
        HideMend.MendHide -= HideMendUI;
    }
    private void showMendUI()
    {
        LeanTween.moveLocalY(MendObject, positionShow, .5f).setEase(LeanTweenType.easeInQuad); // move journal down
    }
    private void HideMendUI()
    {
        LeanTween.moveLocalY(MendObject, positionHide, .5f).setEase(LeanTweenType.easeInQuad); // move journal up
    }
}
