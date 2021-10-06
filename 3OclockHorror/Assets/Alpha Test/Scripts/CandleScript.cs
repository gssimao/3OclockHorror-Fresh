﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public bool SkullCandle;
    public GameObject player;
    public GameObject Light;
    public GameObject Flicker;
    public GameObject lightEffect;
    public SpriteMask LightMask;
    public Light flame; //Variable to that holds the light component of the game object
    [SerializeField]
    bool LeaveOn = false; // Tick this if you want to leave light on during start up

    public float interRange;

    UniversalControls uControls;

    AudioManager manager;
    float dist;

    public bool lightOn;
    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        flame = Light.GetComponent<Light>(); //gets the light component of the child of this game object and sets it to the variable
        if (!LeaveOn)
        {
            CandleToggle(false);
        }
        else
        {
            lightOn = true;
        }
        manager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, this.transform.position);

        if(dist <= interRange && uControls.Player.Light.triggered && !SkullCandle)
        {
            if (flame.isActiveAndEnabled == false)
            {
                CandleToggle(true);
                if(manager != null)
                {
                    manager.Play("Candle Light", true);
                }
            }
            else
            {
                CandleToggle(false);
            }
        }
    }

    void OnDrawGizmos()// Draws a blue circle around the candle in the editor to help visualize the disance of the interactableRange
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, interRange);
    }

    public void CandleToggle(bool trigger)
    {
        if (trigger == false)
        {
            if (flame != null && LightMask != null && Flicker != null)
            {
                flame.enabled = false;
                LightMask.enabled = false;
                Flicker.SetActive(false);
                lightEffect.SetActive(false);
            }
            else if (flame != null && LightMask != null)
            {
                flame.enabled = false;
                LightMask.enabled = false;
                lightEffect.SetActive(false);
            }

            lightOn = false;
        }
        else if(trigger == true)
        {
            if (flame != null && LightMask != null && Flicker != null)
            {
                flame.enabled = true;
                LightMask.enabled = true;
                Flicker.SetActive(true);
                lightEffect.SetActive(true);
            }
            else if (flame != null && LightMask != null)
            {
                flame.enabled = true;
                LightMask.enabled = true;
                lightEffect.SetActive(true);
            }

            lightOn = true;
        }
    }

    public void setPlayerObject(GameObject input)// used for sceneManager script
    {
        player = input;
    }
    public GameObject getPlayerObject()
    {
        return player;
    }
}
