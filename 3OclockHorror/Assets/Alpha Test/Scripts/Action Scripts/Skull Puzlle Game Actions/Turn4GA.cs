using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn4GA : GameActions
{
    public Animator SkullRotation1;
    public Animator SkullRotation3;
    public Animator SkullRotation4;

    [SerializeField] UpdatePillarStage UpdatePillar;
    public override void Action()
    {
        if (!UpdatePillar.stg3)
        {
            UpdatePillar.SkullPosition1++;
            UpdatePillar.SkullPosition3++;
            UpdatePillar.SkullPosition4++;
            if (UpdatePillar.SkullPosition1 > 3)
            {
                UpdatePillar.SkullPosition1 = 0;
            }
            if (UpdatePillar.SkullPosition3 > 3)
            {
                UpdatePillar.SkullPosition3 = 0;
            }
            if (UpdatePillar.SkullPosition4 > 3)
            {
                UpdatePillar.SkullPosition4 = 0;
            }
            SkullRotation1.SetInteger("SkullPosition", UpdatePillar.SkullPosition1);
            SkullRotation3.SetInteger("SkullPosition", UpdatePillar.SkullPosition3);
            SkullRotation4.SetInteger("SkullPosition", UpdatePillar.SkullPosition4);
        }
    }
}
