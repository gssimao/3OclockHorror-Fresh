using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObject : GameActions
{


    public override void Action()
    {
        gameObject.SetActive(true);
    }
}
