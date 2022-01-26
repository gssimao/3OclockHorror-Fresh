using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerAnimationUpGA : GameActions
{
    [SerializeField] private Image InteractDrawer;
    private float positionTop = 430;
    public override void Action()
    {
        LeanTween.moveLocalY(InteractDrawer.gameObject, positionTop, .5f).setEase(LeanTweenType.easeInQuad);
        Debug.Log("Running DrawerAnimation UP");
        
    }
}
