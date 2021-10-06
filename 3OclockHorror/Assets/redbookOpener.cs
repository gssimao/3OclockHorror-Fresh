﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redbookOpener : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bookCanv;
    [Space]
    [SerializeField]
    invInput Listener;
    [Space]
    [SerializeField]
    TaskListTracker taskList;
    bool msgSent = false;

    UniversalControls uControls;
    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if(dist < 0.25)
        {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered && !bookCanv.activeSelf)
            {
                bookCanv.SetActive(true);
                if(msgSent == false)
                {
                    taskList.updateList("\n - The book appears to have a weird symbol, and part of another one - I wonder what they do");
                    msgSent = true;
                }
            }
        }
    }
}
