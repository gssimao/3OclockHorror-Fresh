using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowPlayerMovementGA : GameActions
{
    [SerializeField] PlayerMovement playerMovement;
    public override void Action()
    {
        playerMovement.ChangeCanMove(true);
    }

}
