﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullInteract : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    SkullTurning turner;
    [Space]
    [SerializeField]
    int skull;
    [SerializeField]
    KeyCode interactKey;
    [SerializeField]
    invInput Listener;
    AudioManager manager;
    UniversalControls uControls;
    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
        manager = FindObjectOfType<AudioManager>();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, this.transform.position);
        
        if (dist <= 0.25f) {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered) {
                manager.Play("Skull turn", true);
                switch (skull)
                {
                    case 1:
                        turner.Turning1();
                        break;
                    case 2:
                        turner.Turning2();
                        break;
                    case 3:
                        turner.Turning3();
                        break;
                    case 4:
                        turner.Turning4();
                        break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
    }
}
