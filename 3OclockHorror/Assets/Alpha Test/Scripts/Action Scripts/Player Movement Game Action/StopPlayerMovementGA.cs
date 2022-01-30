using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovementGA : GameActions
{
    [SerializeField] PlayerMovement playerMovement;
    public override void Action()
    {
        playerMovement.ChangeCanMove(false);
    }
}
