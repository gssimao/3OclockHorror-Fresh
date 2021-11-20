﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntCheckSolved : MonoBehaviour
{
    public sendMessage NeedKeyMessage;
    public GameObject Player;
    public GameObject TP;
    public GameObject Gear1;
    public GameObject Gear2;
    public GameObject Gear3;
    private GameObject timerObject;
    public TrapController TrapCtrl;
    public room basement;

    private int answer1;
    private int answer2;
    private int answer3;

    public PlayerMovement playermovement;
    public int showanswer1;
    public int showanswer2;
    public int showanswer3;

    public Text Text1;
    public Text Text2;
    public Text Text3;
    public GameObject Solved;
    public Text SolvedText;
    public Text TimerText;
    public bool timeractive;
    public GameObject JumpscareCanvas;
    private GameObject ExitButton;
    private GameObject DisableOldBearTrap;
    public Sprite broken;

    public bool solved = false;
    //public GameObject clock;
    public bool timercheck;

    bool endTriggered = false;

    private float timer = 10.0f;
    public bool lost = false;

    AudioManager manager;
    [SerializeField]
    FloorAudioController SoundTrack;
    public bool HunterTrapActive = false; // When this is true, the SoundTrack plays. You can use it or anything else though as well.

    [Space]
    [SerializeField]
    bool isTutorial = false;
    [SerializeField]
    Item key;
    [SerializeField]
    GameObject KeyPopUp;

    //[SerializeField]
    //GameObject isSolved;

    private void Start()
    {
        RestartingPuzzle();
        
    }

    public void Awake()
    {
        timeractive = false;
        timerObject = GameObject.Find("Timer");
        ExitButton = GameObject.Find("ExitButton");
        manager = FindObjectOfType<AudioManager>();
        if (GameObject.Find("Jumpscare") != null)
        {
            JumpscareCanvas = GameObject.Find("Jumpscare");
            JumpscareCanvas.SetActive(false);
            ExitButton.SetActive(false);
        }
        else
        {
            ExitButton.SetActive(true);
        }
        GameObject.Find("SolvedText").SetActive(false);
        GameObject.Find("BeartrapPuzzle").SetActive(false);

    }

    public void checkAnswer()
    {
        if (Gear1.GetComponent<GearRotation>().movement == answer1 && Gear2.GetComponent<GearRotation>().movement == answer2 && Gear3.GetComponent<GearRotation>().movement == answer3 && !lost)
        {

            timer = 15f;
            ExitButton.gameObject.SetActive(true);
            solved = true;
            Solved.SetActive(true);
            GameObject.Find("BeartrapPuzzle").SetActive(false);
            if(!isTutorial)
                DisableOldBearTrap.GetComponent<ActivateBeartrap>().ChangeTrigger(false, broken);

            if (!isTutorial)
            {
                manager.Stop("Hunter");
                SoundTrack.StopALL = false;
                SoundTrack.CheckFloor();
            }
            manager.Play("Success", false);

            if(isTutorial)
            {
                if (!playermovement.plyInv.ContainsItem(key))
                {
                    KeyPopUp.SetActive(true);
                    NeedKeyMessage.TriggerMessage();
                }
                else
                {
                    EnterTheHouse ETC = new EnterTheHouse();
                    StartCoroutine(ETC.LoadYourAsyncScene());
                }
            }
        }
    }
    public void SetSolveToFalse()
    {
        solved = false;
        lost = false;
        if(!isTutorial)
        {
            timeractive = true;
            timercheck = true;
        }
    }
    public void ExitPuzzle()
    {
        if (TrapCtrl != null)
        {
            TrapCtrl.DeactivateTraps();
        }
        if(!isTutorial)
            RestartingPuzzle();
        GameObject.Find("BeartrapPuzzle").SetActive(false);

    }

    public void Activate(GameObject Puzzle, GameObject UsedBearTrap)
    {
        DisableOldBearTrap = UsedBearTrap;
        if (!solved)
        {
            HunterTrapActive = true;
            Puzzle.SetActive(true);
            if (!timercheck)
            {
                timerObject.SetActive(false);
                Debug.Log("False TimerCheck");
            }
            else
            {
                Debug.Log("True TimerCheck");
                timeractive = true;
            }
            if (!isTutorial)
            {
                SoundTrack.StopSoundTrack();
                manager.Play("Hunter", false);
            }
        }
        HunterTrapActive = false;
    }
    public void RestartingPuzzle()
    {
        SetSolveToFalse();
        answer1 = Random.Range(0, 5);
        answer2 = Random.Range(0, 5);
        answer3 = Random.Range(0, 5);
        while (answer2 == answer1 && answer3 == answer1 && answer3 == answer2)
        {
            answer1 = Random.Range(0, 5);
            answer2 = Random.Range(0, 5);
            answer3 = Random.Range(0, 5);
        }

        showanswer1 = answer1 + 1;
        showanswer2 = answer2 + 1;
        showanswer3 = answer3 + 1;

        Text1.text = showanswer1.ToString();
        Text2.text = showanswer2.ToString();
        Text3.text = showanswer3.ToString();
        if (timer < 1 && !isTutorial)
        {
            timer = 15f;
        }

        if(!isTutorial) // i've nested this because this is being called at the tutorial and is currently missing the TrapCtrl.triggered reference
        {
            if (TrapCtrl.triggered == 0) // the ideal solution is to make the scripts independent later
            {
                timer = 15.0f;
            }
            TrapCtrl.triggered++;
        }
        
       
        if(!isTutorial)
            ExitButton.gameObject.SetActive(false);
        //TimerText = GameObject.Find("Timer").Text;
    }
    void Update()
    {
        if (isTutorial)
            timer = 0;

        if (!lost && !solved && timercheck && timeractive)
        {
            solved = false;
            timer -= Time.deltaTime;
            TimerText.text = System.Math.Round(timer,2).ToString();
            ExitButton.gameObject.SetActive(false);
            if (timer <= 0)
            {
                if (!isTutorial)
                {
                    manager.Stop("Hunter");
                    SoundTrack.StopALL = false;
                    SoundTrack.CheckFloor();
                }
                ExitButton.gameObject.SetActive(true);
                if(!isTutorial)
                    lost = true;
                //TrapCtrl.DeactivateTraps();
                if (!isTutorial)
                    JumpscareCanvas.SetActive(true);
                Player.transform.position = TP.transform.position;
                playermovement.myRoom = basement;
                playermovement.enabled = true;
                GameObject.Find("BeartrapPuzzle").SetActive(false);
                RestartingPuzzle();
                //SolvedText.text = "Lost";
                //Solved.SetActive(true);
                //GameObject.Find("AnswerButton").SetActive(false);
            }
            
        }

    }
}
