using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendMessage : MonoBehaviour
{
    public Message message;
    public GameObject CallHouse;

    bool active = false;
    public bool instaPlay;
    public bool playOnlyOnce = true;
    public bool destroyMessage = false;
    public bool StatueRoomMessageTriger = false;
    //public bool useCreepyFont = false;

    private void Awake() // if true, it makes the message play instantly when the object attached is set to gameObject.SetActive(true);
    {
        if(instaPlay && playOnlyOnce)
        {
            PlayOnlyOnceTrigger();
           // TriggerMessage();
        }
        else if (instaPlay)
        {
            TriggerMessage();
        }
    }

    /*        
       float timer = 5f;
        public bool useTimer = false;
        private void Update() // bad prototype for using time to trigger a message
        {
            if(useTimer)
            {
                timer -= Time.deltaTime;
                if(timer <= 0 )
                {
                    TriggerMessage();
                    useTimer = false;
                }
            }
        }*/

    public void TriggerMessage()
    {
        CallHouse.GetComponent<CallHouseText>().SetActivateAndGrabString(message, false);
        if(destroyMessage)
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayOnlyOnceTrigger()
    {
        if(playOnlyOnce)
        {
            CallHouse.GetComponent<CallHouseText>().SetActivateAndGrabString(message, false);
            playOnlyOnce = false;
        }
        
        if (destroyMessage)
        {
            Destroy(this.gameObject);
        }
    }
    public void TriggerCreepyFont()
    {
        if (playOnlyOnce)
        {
            CallHouse.GetComponent<CallHouseText>().SetActivateAndGrabString(message, true);
            playOnlyOnce = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.gameObject.tag == "Message")
        {
            TriggerMessage();
            active = true;
            Debug.Log("Triggered");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}

