using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryingToLeaveEndingRoom : MonoBehaviour
{
    public sendMessage TryingtoLeaveMessage;
    public sendMessage fatherMessage;
    [SerializeField]
    GameObject player;
    UniversalControls uControls;


    public float dist;
    float range = 0.5f;

    private void Awake()
    {
       
        uControls = new UniversalControls();
        uControls.Enable();
    }

    void Update()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= range)
        {
            if (uControls.Player.Interact.triggered)
            {
                TryingtoLeaveMessage.PlayOnlyOnceTrigger();
                fatherMessage.TriggerCreepyFont();
            }
        }
    }

}
