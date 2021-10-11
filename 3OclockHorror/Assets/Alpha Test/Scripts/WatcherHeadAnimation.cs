using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherHeadAnimation : MonoBehaviour
{
    public GameObject player;
    public float plyAngle = 0;
    public Animator watcherAnim;


    void Update()
    {
        UpdateFace();
    }


    void UpdateFace()
    {
        Vector3 direction = player.transform.position - this.gameObject.transform.position;
        plyAngle = Vector3.Angle(direction, this.gameObject.transform.right);

        if (plyAngle <= 180 && plyAngle > 121)
        {
            watcherAnim.SetTrigger("Left");
        }
        else if (plyAngle <= 120 && plyAngle > 61)
        {
            watcherAnim.SetTrigger("Forward");
        }
        else if (plyAngle <= 60 && plyAngle > 0)
        {
            watcherAnim.SetTrigger("Right");
        }
    }



}
