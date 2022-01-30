using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowJournal : GameActions
{    
    public static Action Show = delegate { };
    public override void Action()
    {
        Show();
    }
}
