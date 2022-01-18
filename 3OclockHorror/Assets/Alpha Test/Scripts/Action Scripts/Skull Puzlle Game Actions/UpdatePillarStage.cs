using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePillarStage : GameActions
{
    public sendMessage EndPuzzleMessage;
    public int SkullPosition1 = 0;
    public int SkullPosition2 = 0;
    public int SkullPosition3 = 0;
    public int SkullPosition4 = 0;
    [Space]
    public pillarCntrl pillarDisplay;

    public GameActionTrigger Candle1;
    public GameActionTrigger Candle2;
    public GameActionTrigger Candle3;

    //Stage booleans
    public bool stg1 = false;
    public bool stg2 = false;
    public bool stg3 = false;
    public override void Action()
    {
        if (!stg3)
        {
            if (SkullPosition1 == 2 && SkullPosition2 == 0 && SkullPosition3 == 2 && SkullPosition4 == 0 && !stg1)
            {
                stg1 = true;
                pillarDisplay.updatePilar("stage1");
                Debug.Log("Stg 1 Complete");
                //manager.Play("Skull light", true);
                Candle2.Execute();
                Candle1.Execute();
            }
            if (SkullPosition1 == 1 && SkullPosition2 == 1 && SkullPosition3 == 3 && SkullPosition4 == 3 && stg1)
            {
                stg2 = true;
                pillarDisplay.updatePilar("stage2");
                Debug.Log("Stg 2 complete");
                //manager.Play("Skull light", true);
                Candle2.Execute();
                Candle3.Execute();
            }
            if (SkullPosition1 == 0 && SkullPosition2 == 2 && SkullPosition3 == 0 && SkullPosition4 == 2 && stg1 && stg2)
            {
                stg3 = true;
                pillarDisplay.updatePilar("stage3");
                Debug.Log("Stg 3 complete, locking puzzle");
                //manager.Play("Skull success", false);
                Candle3.Execute();
                this.GetComponent<SkullEnd>().OpenLeftEnding();
                EndPuzzleMessage.TriggerMessage();

            }
        }
    }
}
