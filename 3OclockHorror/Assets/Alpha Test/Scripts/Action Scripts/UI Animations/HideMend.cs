using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideMend : GameActions
{
    public static Action MendHide = delegate { };
    public override void Action()
    {
        MendHide();
    }
}
