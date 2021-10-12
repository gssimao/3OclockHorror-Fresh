using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingFade : MonoBehaviour
{
    
    public endScreenControl endController;
    int endingCondition = 0;
    public float CalculateInGameTime;
    public GameObject CloseinHouse;
    public GameObject HouseFar;
    public GameObject HouseFarWithMask;
    public GameObject Fence;
    public GameObject Lamp;
    public GameObject MasksBackground;
    public GameObject BigMask;
    public AudioManager manager;
    public sendMessage StartMessage;
    public sendMessage EndingMessage1;
    public sendMessage EndingMessage2;
    public sendMessage EndingMessage3;
    public GameObject ProgresButtons;
    public Text CongratulationMessage;
    public Text inGameTime;

    private void Awake()
    {
        //============ UNCOMMENT THIS TO WORK
        endController = GameObject.Find("EndScreenController").GetComponent<endScreenControl>();
        endingCondition = endController.endingCondition;
        CalculateInGameTime = endController.Finaltime;

        StartMessage.PlayOnlyOnceTrigger();

        manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        manager.StopAll();
        
        //==============================================
        //for testing
        /*endingCondition = 3;
        CalculateInGameTime = 1250;*/
        //==============================================

        switch (endingCondition)
        { 
        case 1:
                StartCoroutine(ending1Execution());
                break;

        case 2:
                StartCoroutine(ending2Execution());
                break;
            
        case 3:
                StartCoroutine(ending3Execution());
                break;
        
        default:
                // send to gameOverScene
                Debug.LogError("The EndCondition is not valid. As follow ---> " + endingCondition);
                SceneManager.LoadScene("End_Scene_Alpha");
                break;

        }

    }


    IEnumerator ending1Execution() // escaped with the diamond
    {
        manager.Play("Theme", false);

        FadeIn(CloseinHouse.GetComponent<Image>(), 10);
        Zoom(CloseinHouse.GetComponent<Image>(), CloseinHouse.transform.localScale, -.03f, 15);
        yield return new WaitForSeconds(10);//yield on a new YieldInstruction that waits for 5 seconds.
        FadeOut(CloseinHouse.GetComponent<Image>(), 5);

        FadeIn(HouseFar.GetComponent<Image>(), 10);
        Zoom(HouseFar.GetComponent<Image>(), HouseFar.transform.localScale, -.04f, 10);

        FadeIn(Fence.GetComponent<Image>(), 12);
        Zoom(Fence.GetComponent<Image>(), Fence.transform.localScale, -.03f, 12);

        FadeIn(Lamp.GetComponent<Image>(), 14);
        Zoom(Lamp.GetComponent<Image>(), Lamp.transform.localScale, -.02f, 14);

        yield return new WaitForSeconds(8);

        EndingMessage1.PlayOnlyOnceTrigger();

        ProgresButtons.SetActive(true);

        CongratulationMessage.text = "The Night Robber.\nFinal Time:";
        inGameTime.text = CalculateEndTime(CalculateInGameTime);
        Debug.Log("inside ending 1 Complete");
    }


    IEnumerator ending2Execution()
    {
        manager.Play("Theme", false);
        //==========================

        FadeIn(CloseinHouse.GetComponent<Image>(), 10);
        Zoom(CloseinHouse.GetComponent<Image>(), CloseinHouse.transform.localScale, .03f, 15);
        yield return new WaitForSeconds(10);//yield on a new YieldInstruction that waits for 5 seconds.
        FadeOut(CloseinHouse.GetComponent<Image>(), 5);

        FadeIn(HouseFarWithMask.GetComponent<Image>(), 10);
        Zoom(HouseFarWithMask.GetComponent<Image>(), HouseFar.transform.localScale, .09f, 120);

        FadeIn(MasksBackground.GetComponent<Image>(),20, .14f);
        Zoom(HouseFar.GetComponent<Image>(), HouseFar.transform.localScale, .05f, 100);

        FadeIn(Lamp.GetComponent<Image>(), 14);
        Zoom(Lamp.GetComponent<Image>(), Lamp.transform.localScale, .03f, 14);

        //==============
        yield return new WaitForSeconds(6);
        EndingMessage2.PlayOnlyOnceTrigger();
        ProgresButtons.SetActive(true);

        CongratulationMessage.text = "The Night Dweller.\nFinal Time:";
        inGameTime.text = CalculateEndTime(CalculateInGameTime);
        Debug.Log("inside ending 2");
    }
    IEnumerator ending3Execution()
    {
        manager.Play("Theme", false);

        //==========================

        FadeIn(CloseinHouse.GetComponent<Image>(), 10);
        Zoom(CloseinHouse.GetComponent<Image>(), CloseinHouse.transform.localScale, -.05f, 16);
        yield return new WaitForSeconds(11);//yield on a new YieldInstruction that waits for 5 seconds.
        FadeOut(CloseinHouse.GetComponent<Image>(), 5);

        FadeIn(HouseFar.GetComponent<Image>(), 10);
        Zoom(HouseFar.GetComponent<Image>(), HouseFar.transform.localScale, -.05f, 100);

        FadeIn(Fence.GetComponent<Image>(), 12);
        Zoom(Fence.GetComponent<Image>(), Fence.transform.localScale, -.03f, 12);

        FadeIn(Lamp.GetComponent<Image>(), 14);
        Zoom(Lamp.GetComponent<Image>(), Lamp.transform.localScale, -.02f, 14);

        //==============

        yield return new WaitForSeconds(5);
        EndingMessage3.PlayOnlyOnceTrigger();
        ProgresButtons.SetActive(true);

        CongratulationMessage.text = "The Night Cleanser.\nFinal Time:";
        inGameTime.text = CalculateEndTime(CalculateInGameTime);
        Debug.Log("inside ending 3");
    }

    private void FadeIn(Image image, float time)
    {
        LeanTween.alpha(image.rectTransform, 1f, time).setEaseInOutBack();
    }
    private void FadeIn(Image image, float time, float alpha)
    {
        LeanTween.alpha(image.rectTransform, alpha, time).setEaseInOutBack();
    }
    private void FadeOut(Image image, float time)
    {
        LeanTween.alpha(image.rectTransform, 0f, time).setEaseInOutBack();
    }
    private void Zoom(Image image, Vector3 CurrentPos ,float zoomMove , float time)
    {
        LeanTween.scale(image.rectTransform, CurrentPos + new Vector3(zoomMove, zoomMove, 0), time).setEaseInOutBack();
    }
    public string CalculateEndTime(float EndTime)
    {
        string minutes = Mathf.Floor(EndTime / 60).ToString("00");
        string seconds = Mathf.Floor(EndTime % 60).ToString("00");
        string niceTime = string.Format("Final Time {0:0}m:{1:00}s", minutes, seconds);
        return niceTime;
    }
}
