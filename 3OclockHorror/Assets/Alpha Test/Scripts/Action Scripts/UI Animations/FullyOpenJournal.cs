using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FullyOpenJournal : GameActions
{
    public static Action<int> FullShow = delegate { };
    public override void Action()
    {
        FullShow(0);
    }

}
