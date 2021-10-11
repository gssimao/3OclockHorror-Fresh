using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighEndingNoDiamondSequence : MonoBehaviour
{
    public GameObject candle1;
    public GameObject candle2;
    public GameObject candle3;
    public GameObject candle4;
    public GameObject candle5;
    public GameObject candle6;
    public GameObject candle7;
    public GameObject candle8;
    private float timer;
    private int totalCandles = 8;
    private bool stop = false;
    private void ResetTimer()
    {
        if(totalCandles>0)
            timer = 10f;

        totalCandles -= 1;
    }
    private void turnOffCandles(int candle)
    {
        if (candle == 8)
        {
            candle1.SetActive(false);
        }
        else if (candle == 7)
        {
            candle2.SetActive(false);
        }
        else if (candle == 6)
        {
            candle3.SetActive(false);
        }
        else if (candle == 5)
        {
            candle4.SetActive(false);
        }
        else if (candle == 4)
        {
            candle5.SetActive(false);
        }
        else if (candle == 3)
        {
            candle6.SetActive(false);
        }
        else if (candle == 2)
        {
            candle7.SetActive(false);
        }
        else if (candle == 1)
        {
            candle8.SetActive(false);
        }
        else if(candle < 1)
        {
            stop = true;
        }

    }
    void Update()
    {
        if(stop == false)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                ResetTimer();
            }
        }

    }
}
