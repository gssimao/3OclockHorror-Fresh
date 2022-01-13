using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandleGA : GameActions
{
    public bool SkullCandle;
    public GameObject Light;
    public GameObject Flicker;
    public GameObject lightEffect;
    public SpriteMask LightMask;
    public Light flame; //Variable to that holds the light component of the game object
    /*[SerializeField]
    bool LeaveOn = false; // Tick this if you want to leave light on during start up
    private bool EternalLight = false; //make a candle last forever once lit*/
    AudioManager manager;
    public bool lightOn;

    public override void Action()
    {
        if (lightOn == true) //the light is on and we want to turn if off
        {
            if (flame != null && LightMask != null)
            {
                flame.enabled = false;
                LightMask.enabled = false;
                lightEffect.SetActive(false);
            }
            if (Flicker != null)
            {
                Flicker.SetActive(false);
            }
            lightOn = false;
            Debug.Log("Complete lignt off");

        }
        else  //the light is off and we want to turn if on
        {
            if(manager == null)
            {
                manager = FindObjectOfType<AudioManager>();
                manager.Play("Candle Light", true);
            }
            else
            {

                manager.Play("Candle Light", true);
            }
            

            if (flame != null && LightMask != null)
            {
                flame.enabled = true;
                LightMask.enabled = true;
                lightEffect.SetActive(true);
            }
            if (Flicker != null)
            {
                Flicker.SetActive(true);
            }
            lightOn = true;
            Debug.Log("Complete lignt ON");
        }
    }
    

}
