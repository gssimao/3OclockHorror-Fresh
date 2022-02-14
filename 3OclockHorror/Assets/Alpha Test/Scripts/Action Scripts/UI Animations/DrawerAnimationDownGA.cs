using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerAnimationDownGA : GameActions
{
    [SerializeField] private Image InteractDrawer;
    public float positionBot = 190; //-139
    public override void Action()
    {
        LeanTween.moveLocalY(InteractDrawer.gameObject, positionBot, .5f).setEase(LeanTweenType.easeInQuad);
       // Debug.Log("Running DrawerAnimation Down");
    }
}
