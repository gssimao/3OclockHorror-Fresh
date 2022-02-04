using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowInteractionGA : GameActions
{
    public static Action ShowInteractButton = delegate { };
    public override void Action()
    {
        ShowInteractButton();
    }
}
