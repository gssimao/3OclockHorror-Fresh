using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn3GA : GameActions
{
    public Animator SkullRotation1;
    public Animator SkullRotation4;

    [SerializeField] UpdatePillarStage UpdatePillar;
    public override void Action()
    {
        if (!UpdatePillar.stg3)
        {
            UpdatePillar.SkullPosition1++;
            UpdatePillar.SkullPosition4++;
            if (UpdatePillar.SkullPosition1 > 3)
            {
                UpdatePillar.SkullPosition1 = 0;
            }
            if (UpdatePillar.SkullPosition4 > 3)
            {
                UpdatePillar.SkullPosition4 = 0;
            }
            SkullRotation1.SetInteger("SkullPosition", UpdatePillar.SkullPosition1);
            SkullRotation4.SetInteger("SkullPosition", UpdatePillar.SkullPosition4);
        }
    }
}
