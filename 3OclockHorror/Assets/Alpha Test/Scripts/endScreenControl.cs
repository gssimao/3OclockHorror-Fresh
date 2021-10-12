using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreenControl : MonoBehaviour
{
    public static endScreenControl instance;
    public string endMessage;
    public int endingCondition = 0;
    public float Finaltime = 0;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void TriggerEnding(int condition)
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player2");
        }
        endingCondition = condition;
        Finaltime = Player.GetComponent<clockCntrl>().Gettime();
        SceneManager.LoadScene("Ending");
    }

    public void PassEndingTime(float time) 
    {
        Finaltime = time;
    }
}
