using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public bool SkullCandle;
    public GameObject Light;
    public GameObject Flicker;
    public GameObject lightEffect;
    public SpriteMask LightMask;
    public Light flame; //Variable to that holds the light component of the game object
    [SerializeField]
    bool LeaveOn = false; // Tick this if you want to leave light on during start up
    private bool EternalLight = false; //make a candle last forever once lit

    UniversalControls uControls;
    public invInput Listener;
    AudioManager manager;

    public bool lightOn;
    private void Awake()
    {
        Listener = GameObject.Find("Listener").GetComponent<invInput>();
        uControls = new UniversalControls();
        uControls.Enable();

        flame = Light.GetComponent<Light>(); //gets the light component of the child of this game object and sets it to the variable
        if (!LeaveOn)
        {
            lightOn = true;
            CandleToggle();//the lights will be turned off
        }
        else
        {
            lightOn = true;
        }
        manager = FindObjectOfType<AudioManager>();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    public void CandleToggle()
    {
        
        if (lightOn == true) //the light is on and we want to turn if off
        {
            if (flame != null && LightMask != null)
            {
                flame.enabled = false;
                LightMask.enabled = false;
                lightEffect.SetActive(false);
            }
            if(Flicker != null)
            {
                Flicker.SetActive(false);
            }
            lightOn = false;
            //Debug.Log("Complete lignt off");

        }
        else  //the light is off and we want to turn if on
        {
            manager.Play("Candle Light", true);
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
           //Debug.Log("Complete lignt ON");
        }
    }
    public void CandleToggle(bool state)
    {
        if (flame != null && LightMask != null)
        {
            flame.enabled = state;
            LightMask.enabled = state;
            lightEffect.SetActive(state);
        }
        if (Flicker != null)
        {
            Flicker.SetActive(state);
        }
        lightOn = state;
    }
 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        Listener.CandleSwitch(true);
        LightCandle.TriggerCandle += CandleToggle;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Listener.CandleSwitch(false);
        LightCandle.TriggerCandle -= CandleToggle;
    }*/

    /*  public void setPlayerObject(GameObject input)// used for sceneManager script
      {
          player = input;
      }
      public GameObject getPlayerObject()
      {
          return player;
      }*/

}
