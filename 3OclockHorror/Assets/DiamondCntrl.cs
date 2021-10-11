using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCntrl : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject EndCanv;
    [SerializeField]
    public endScreenControl endController;

    public invInput Listener;
    bool diamondTaken = false;

    UniversalControls uControls;
    private void Awake()
    {
        endController = GameObject.Find("EndScreenController").GetComponent<endScreenControl>();
        uControls = new UniversalControls();
        uControls.Enable();
        
}
    private void OnDisable()
    {
        uControls.Disable();
    }
    public void triggerEnding1()
    {
        endController.TriggerEnding(1);
        endController.PassEndingTime(player.GetComponent<clockCntrl>().Gettime());
        player.GetComponent<clockCntrl>().StopTime(false);// resuming time
    }
    public void ResumeTime()
    {
        //continue clock
        player.GetComponent<clockCntrl>().StopTime(false);//called in gameObject endcanvas button
    }
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf && !diamondTaken)
        {
            float dist = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
            if(dist < 1)
            {
                //Listener.isFocus = false;
                if (uControls.Player.Interact.triggered)
                {
                    diamondTaken = true;
                    EndCanv.SetActive(true);
                    player.GetComponent<clockCntrl>().StopTime(true);
                }       
            }
            player.GetComponent<PlayerMovement>().leftSide = true;
        }        
    }
}
