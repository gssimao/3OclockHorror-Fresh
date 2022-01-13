using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectGA : GameActions
{
   public GameObject gameObject;

    public override void Action()
    {
        gameObject.SetActive(false);
    }
}
