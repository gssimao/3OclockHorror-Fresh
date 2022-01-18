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
    public Light flame; //Variable to that holds the light component of the game object
    public bool EternalLight = false;
    /*[SerializeField]
    bool LeaveOn = false; // Tick this if you want to leave light on during start up
    private bool EternalLight = false; //make a candle last forever once lit*/
    AudioManager manager;
    public bool lightOn;

    public override void Action()
    {
        if (lightOn == false && EternalLight == false) //the light is on and we want to turn if off
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

            flame.enabled = true;
            LightMask.SetActive(true);
            lightEffect.SetActive(true);
            Flicker.SetActive(true);

            lightOn = true;

        }
        else  //the light is off and we want to turn if on
        {
            flame.enabled = false;
            LightMask.SetActive(false);
            lightEffect.SetActive(false);
            Flicker.SetActive(false);

            lightOn = false;
        }
    }

}
