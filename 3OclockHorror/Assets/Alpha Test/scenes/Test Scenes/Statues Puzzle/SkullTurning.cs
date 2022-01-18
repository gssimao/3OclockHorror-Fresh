using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTurning : MonoBehaviour
{
    public sendMessage EndPuzzleMessage;
    public Animator SkullRotation1;
    public Animator SkullRotation2;
    public Animator SkullRotation3;
    public Animator SkullRotation4;
/*    public int SkullPosition1 = 0;
    public int SkullPosition2 = 0;
    public int SkullPosition3 = 0;
    public int SkullPosition4 = 0;*/
    [Space]
    public pillarCntrl pillarDisplay;

    public CandleScript cn1;
    public CandleScript cn2;
    public CandleScript cn3;

    //Stage booleans
    bool stg1 = false;
    bool stg2 = false;
    bool stg3 = false;
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
/*        cn1.CandleToggle(false);
        cn2.CandleToggle(false);
        cn3.CandleToggle(false);*/
    }
    

    
}
