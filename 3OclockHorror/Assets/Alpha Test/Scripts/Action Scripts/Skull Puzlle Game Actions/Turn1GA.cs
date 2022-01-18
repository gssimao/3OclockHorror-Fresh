using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn1GA : GameActions
{
    [SerializeField] UpdatePillarStage UpdatePillar;
    public Animator SkullRotation1;
    public override void Action()
    {
        if (!UpdatePillar.stg3)
        {
            UpdatePillar.SkullPosition1++;
            if (UpdatePillar.SkullPosition1 > 3)
            {
                UpdatePillar.SkullPosition1 = 0;
            }
            SkullRotation1.SetInteger("SkullPosition", UpdatePillar.SkullPosition1);
        }
    }
}
