using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DoorTeleport : GameActions
{
    public GameObject DestinationRoomObject;
    [SerializeField] ContainerItems PlayerInventory;
    [SerializeField] GameObject RomanNumeralLockerCanv;
    public List<Item> Key;
    public bool RomanNumeralLocker;
    private float transitionTime = .3f;
    private Image BlackBackground;
    public SpriteRenderer LocalRoomSprite;
    public Sprite NewRoomSprite;

    public override void Action()
    {
        if(BlackBackground == null)
            BlackBackground = GameObject.Find("TransitionPanel").GetComponent<Image>();
       // Debug.Log("caling action");
        if (RomanNumeralLocker) // turn on when lock is needed, turn off when the lock is solved
        {
            RomanNumeralLockerCanv.SetActive(true);
            return;
        }
       // Debug.Log("passed roman numeral ready to ckeck keys");

        if (Key.Count > 0)
        {
            bool unlock = CheckKey(); //returns false if player does not have the necessery items to open it.
            if (!unlock)
                return;
            else
                if (NewRoomSprite != null) 
                    LocalRoomSprite.sprite = NewRoomSprite;
        }

        //Debug.Log("pass the key wants to teleport");
        // there is nothing else in the way of the player to get into the other room, teleport the player
        StartCoroutine(ChangeRoom(GameObject.Find("Player2"), DestinationRoomObject, DestinationRoomObject.transform.parent.parent.gameObject.GetComponent<room>()));
    }

    IEnumerator ChangeRoom(GameObject playerObject, GameObject entranceP, room RoomNum)
    {
        //LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).;
        playerObject.GetComponent<PlayerMovement>().myRoom = RoomNum;
        LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).setEaseInBack().setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setOnComplete(() =>
        {
            //TELEPORT ZONE
            playerObject.transform.position = entranceP.transform.position; // take player to the new place
        });

        yield return new WaitForSeconds(transitionTime * 2);
        LeanTween.value(BlackBackground.gameObject, 1, 0, transitionTime * 2).setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setEaseInBack();

    }
    public bool CheckKey()
    {
        //check if player has the necessery things to open a door
        bool pass = PlayerInventory.ContainsItem(Key);
        if (pass)
        {
            foreach(Item item in Key)
            {
                PlayerInventory.RemoveItem(item);
                Key.Remove(item);
                if (Key.Count == 0)
                    break;
            }
        }
        return pass; // returns false if player does not have the necessery items to open it.
    }

}
