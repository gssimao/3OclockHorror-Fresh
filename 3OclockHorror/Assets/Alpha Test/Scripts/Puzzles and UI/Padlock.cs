using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    [SerializeField]
    private DoorTeleport doorTeleportControl;

    public bool solved;
    //Rotating elements of the lock
    [SerializeField]
    LockControl lock1;
    [SerializeField]
    LockControl lock2;
    [SerializeField]
    LockControl lock3;
    [SerializeField]
    LockControl lock4;

    //List of doors to be unlocked when called
    [SerializeField]
    List<roomCntrl> doors;

    //List of photos to check lock against
    public Item Photo1;
    public Item Photo2;
    public Item Photo3;
    public Item Photo4;

    [SerializeField]
    GameObject isSolved;
    [SerializeField]
    Animator lockAnim;

    AudioManager manager;
    public string pass1;
    public string pass2;
    public string pass3;
    public string pass4;


    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
        pass1 = Photo1.numeral;
        pass2 = Photo2.numeral;
        pass3 = Photo3.numeral;
        pass4 = Photo4.numeral;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnswer();
     
    }

    public void CheckAnswer()
    {
        //Before checking, see if the puzzle is solved already
        if (!solved && 
        lock1.numeral == Photo1.numeral && lock2.numeral == Photo2.numeral &&
        lock3.numeral == Photo3.numeral && lock4.numeral == Photo4.numeral) 
        {
            foreach (roomCntrl door in doors) //If they all match, unlock all respective doors assigned to this function
            {
                door.locked = false;
            }
            solved = true;
            doorTeleportControl.RomanNumeralLocker = false;

            lockAnim.SetTrigger("unlock");
            manager.Play("Success", false);
        }
    }
}

