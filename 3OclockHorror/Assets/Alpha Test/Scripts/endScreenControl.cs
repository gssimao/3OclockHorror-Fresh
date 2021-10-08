using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScreenControl : MonoBehaviour
{
    public endScreenControl instance;
    public string endMessage;
    public int endingCondition = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != null)
        {
            Destroy(gameObject); //Is there a manager? If yes then I'm gone
        }
        else
        {
            instance = this;  //There isnt a manager? I'm it
            DontDestroyOnLoad(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    
}
