using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBig : GameActions
{
    public override void Action()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }
}
