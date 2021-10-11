﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorialMessages : MonoBehaviour
{

    GameObject[] tutorialNotes;
    public GameObject player;
    void Awake()
    {
        tutorialNotes = GameObject.FindGameObjectsWithTag("TutorialMessage");
    }

    public void SetTutorilText(bool isActive) //only used during game
    {
        for (int i = 0; i < tutorialNotes.Length; i++)
        {
            tutorialNotes[i].gameObject.SetActive(isActive);
        }
        player.GetComponent<clockCntrl>().StopTutorialNotes(isActive);

    }

}
