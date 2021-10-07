﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallHouseText : MonoBehaviour
{
    int dialogProgress = 0;
    private Queue<string> messageQueue;
    private Writer textWriter;
    public Text TextUi;
    //public AudioSource typewriter;
    //public AudioSource DoneWriting;
    private Writer.TextWriterSingle textWriterSingle;
    public Image blackTop;
    public Image mouse;
    public Button Button;
    public bool destroyBlackTop = false;
    private bool AutoplayText = true;
    float AutoPlayTime = 35f;
    private static float desirableWaitTime = 10f;
    private AudioManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<AudioManager>();
        messageQueue = new Queue<string>();
        Color newColor = blackTop.color;
        newColor.a = 0;                 // changing Alpha to zero
        blackTop.color = newColor;// starting transparent
        mouse.gameObject.SetActive(false);
        blackTop.gameObject.SetActive(false);
        Button.interactable = false;//turn off the button

    }
    private void Autoplay(bool set)
    {
        if(set)
        {
            AutoPlayTime = desirableWaitTime;
        }
        AutoPlayTime -= Time.deltaTime;
        if(AutoPlayTime < 0)
        {
            ShowNewMessage();
            AutoPlayTime = desirableWaitTime;
        }
        
    }
    private void Update()
    {
        if(AutoplayText)
        {
            Autoplay(false);
        }
    }

    public void ShowNewMessage()
    {
        string message; //local string that will pass on the message to the writer
        if (textWriterSingle != null && textWriterSingle.isActive())
        {
            // the writer is active and currently writting
            Debug.Log("Finishing writer");
            textWriterSingle.WriteAndDestroy();
        }
        else
        {
            if (messageQueue.Count == 0)
            {
                CompleteAndTurnOff();
            }
            else
            {
                message = messageQueue.Dequeue();
                StartTypingSound();
                textWriterSingle = Writer.AddWriter_Static(TextUi, message, .05f, true, true, StopTypingSound);
                Autoplay(true);
            }
        }
    }

    public void SetActivateAndGrabString(Message dialogue)
    {

        blackTop.gameObject.SetActive(true);
        Button.interactable = true;
        //Debug.Log("this is message count " + messageQueue.Count);
        if (messageQueue.Count == 0)
        {
            messageQueue.Clear();

            foreach (string message in dialogue.messagesToWrite) // loading the messages
            {
                messageQueue.Enqueue(message);
            }
            ShowNewMessage(); // calling to print the first message
            LeanTween.alpha(blackTop.rectTransform, 1f, .7f);
            mouse.gameObject.SetActive(true);
        }
        else
        {

            foreach (string message in dialogue.messagesToWrite) // loading the messages
            {
                messageQueue.Enqueue(message);
            }
        }
        
    }
    private void CompleteAndTurnOff()
    {
        dialogProgress = 0;
        TextUi.text = "";
        Button.interactable = false;
        //fade out with leanTween
        LeanTween.alpha(blackTop.rectTransform, 0f, .7f);
        mouse.gameObject.SetActive(false);
        if(destroyBlackTop)
        {
            Destroy(blackTop.gameObject);
        }
        blackTop.gameObject.SetActive(false);
    }
    private void StartTypingSound()
    {
        manager.Play("TypeWriter", false);
        //typewriter.Play();
    }
    private void StopTypingSound()
    {
        manager.Stop("TypeWriter");
        //typewriter.Stop();
        //DoneWriting.PlayOneShot(DoneWriting.clip); // this is the Ding sound when the typewriter is done writting
    }
    public void ChangeAutoPlayText( bool change)
    {
        AutoplayText = change;
    }


}
