using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    Text TooltipText;
    [SerializeField]
    GameObject player;
    [SerializeField]
    CanvasGroup cnvGroup;

    public static string Message = "E";

    void Start()
    {
        cnvGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position); //Get the position of player
        Debug.Log(dist);
        if (dist <= .3f)
        {
            TooltipText.text = Message;
            //timer = 1;
            cnvGroup.alpha = 0;
        }
        if (dist >= .3f)
        {
            TooltipText.text = Message;
            //timer = 1;
            cnvGroup.alpha = 1;
        }
       
    }
}