using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarCntrl : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject rightEnding;
    [SerializeField]
    GameObject noDiamond;
    [SerializeField]
    GameObject Diamond;
    [SerializeField]
    invInput Listener;

    public endScreenControl endController;
    public EscCntrl escMenu;
    public sendMessage RighEndingMessageNoDiamond;
    public sendMessage RighEndingMessageWithDiamond;

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

    public void triggerEnding2()
    {
        escMenu.EndingComplete(false);
        endController.TriggerEnding(2);
        endController.PassEndingTime(player.GetComponent<clockCntrl>().Gettime()); // storing the game time 
        player.GetComponent<clockCntrl>().StopTime(false); // resume time in clock script
    }
    public void triggerEnding3()
    {
        escMenu.EndingComplete(false);
        endController.TriggerEnding(3);
        endController.PassEndingTime(player.GetComponent<clockCntrl>().Gettime()); // storing the game time 
        player.GetComponent<clockCntrl>().StopTime(false); // resume time in clock script
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (dist < 1.0f)
        {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered)
            {
                rightEnding.SetActive(true);

                if (player.GetComponent<PlayerMovement>().leftSide)
                {
                    player.GetComponent<clockCntrl>().StopTime(true); // stop all time the player beated the game
                    Diamond.SetActive(true); //calling those canvas
                    RighEndingMessageWithDiamond.TriggerCreepyFont();
                }
                else
                {
                    player.GetComponent<clockCntrl>().StopTime(true);// stop all time the player beated the game
                    noDiamond.SetActive(true); // calling ending 2
                    RighEndingMessageNoDiamond.TriggerMessage();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 1.0f);
    }
}
