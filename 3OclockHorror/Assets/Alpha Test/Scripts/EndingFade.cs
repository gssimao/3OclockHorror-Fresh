using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFade : MonoBehaviour
{
    int endingCondition = 0;
    public endScreenControl endController;
    public GameObject Ending1;
    public GameObject Ending2;
    public GameObject Ending3;
    private void Awake()
    {
        
        GameObject.Find("EndScreenController");

        switch (endingCondition)
        { 
        case 1:
                
            break;

        case 2:

            break;
            
        case 3:

            break;
        
        default:
                // send to gameOverScene
                break;

        }

    }
}
