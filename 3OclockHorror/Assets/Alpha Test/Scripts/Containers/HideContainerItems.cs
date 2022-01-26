using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideContainerItems : GameActions
{
    public static Action HideItems = delegate { };
    public override void Action()
    {
        HideItems();
    }
}
