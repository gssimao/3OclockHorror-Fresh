using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowMend : GameActions
{
    public static Action MendShow = delegate { };
    public override void Action()
    {
        MendShow();
    }
}
