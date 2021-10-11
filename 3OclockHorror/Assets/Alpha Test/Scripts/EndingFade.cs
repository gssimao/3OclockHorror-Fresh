using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingFade : MonoBehaviour
{
    
    public endScreenControl endController;
    int endingCondition = 0;
    public GameObject CloseinHouse;
    public AudioManager manager;
    /*    public Image BackgroundHouse;
        public Image Fence;
        public Image Lamp;




        public GameObject Ending1;
        public GameObject Ending2;
        public GameObject Ending3;
    */
    private void Awake()
    {
        endController = GameObject.Find("EndScreenController").GetComponent<endScreenControl>();
        endingCondition = endController.endingCondition;

        manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        manager.StopAll();
        //for testing
        //endingCondition = 1;
        switch (endingCondition)
        { 
        case 1:
                ending1Execution();
            break;

        case 2:
                ending2Execution();
                break;
            
        case 3:
                ending3Execution();
                break;
        
        default:
                // send to gameOverScene
                Debug.LogError("The EndCondition is not valid. As follow ---> " + endingCondition);
                SceneManager.LoadScene("End_Scene_Alpha");
                break;

        }

    }

    private void ending1Execution()
    {
        manager.Play("Theme", false);
        FadeIn(CloseinHouse.GetComponent<Image>(), 10);
        //FadeOut(CloseinHouse.GetComponent<Image>(), 10);
        Zoom(CloseinHouse.GetComponent<Image>(), CloseinHouse.transform.localScale, -.03f, 15);
        Debug.Log("inside ending 1");
    }
    private void ending2Execution()
    {

    }
    private void ending3Execution()
    {

    }

    private void FadeIn(Image image, float time)
    {
        LeanTween.alpha(image.rectTransform, 1f, time).setEaseInOutBack();
    }
    private void FadeOut(Image image, float time)
    {
        LeanTween.alpha(image.rectTransform, 0f, time).setEaseInOutBack();
    }
    private void Zoom(Image image, Vector3 CurrentPos ,float zoomMove , float time)
    {
        LeanTween.scale(image.rectTransform, CurrentPos + new Vector3(zoomMove, zoomMove, 0), time).setEaseInOutBack();
    }
}
