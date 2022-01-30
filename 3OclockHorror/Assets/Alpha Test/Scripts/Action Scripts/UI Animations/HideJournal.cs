using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideJournal : GameActions
{
    public static Action Hide = delegate { };
    public override void Action()
    {
        Hide();
    }
}
