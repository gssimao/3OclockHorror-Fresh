using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideInteractionGA : GameActions
{
    public static Action HideInteractButton = delegate { };
    public override void Action()
    {
        HideInteractButton();
    }
}
