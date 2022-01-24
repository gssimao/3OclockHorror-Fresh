using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandleGA : GameActions
{
    public bool SkullCandle;
    public GameObject Light;
    public GameObject Flicker;
    public GameObject lightEffect;
    public GameObject LightMask;
    public bool EternalLight = false; //make a candle last forever once lit
    AudioManager manager;
    public bool lightOn;

    public override void Action()
    {
        if (lightOn == false) //the light is off and we want to turn if on
        {
            if (manager == null)
            {
                manager = FindObjectOfType<AudioManager>();
                manager.Play("Candle Light", true);
            }
            else
            {

                manager.Play("Candle Light", true);
            }

            LightMask.SetActive(true);
            lightEffect.SetActive(true);
            Flicker.SetActive(true);
            lightOn = true;

        }
        else  //the light is on and we want to turn if off
        {
            if(!EternalLight)
            {
                LightMask.SetActive(false);
                lightEffect.SetActive(false);
                Flicker.SetActive(false);
                lightOn = false;
            }
            
        }
    }

}
