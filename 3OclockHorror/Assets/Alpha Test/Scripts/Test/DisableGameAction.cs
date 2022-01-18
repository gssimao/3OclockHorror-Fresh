﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameAction : GameActions
{
    public override void Action()
    {
        transform.gameObject.SetActive(false);
    }
}
