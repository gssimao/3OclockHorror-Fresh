using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn2GA : GameActions
{
    public Animator SkullRotation2;
    public Animator SkullRotation4;
    
    [SerializeField] UpdatePillarStage UpdatePillar;
    public override void Action()
    {
        if (!UpdatePillar.stg3)
        {
            UpdatePillar.SkullPosition2++;
            UpdatePillar.SkullPosition4++;
            if (UpdatePillar.SkullPosition2 > 3)
            {
                UpdatePillar.SkullPosition2 = 0;
            }
            if (UpdatePillar.SkullPosition4 > 3)
            {
                UpdatePillar.SkullPosition4 = 0;
            }
            SkullRotation2.SetInteger("SkullPosition", UpdatePillar.SkullPosition2);
            SkullRotation4.SetInteger("SkullPosition", UpdatePillar.SkullPosition4);
        }
    }
}
