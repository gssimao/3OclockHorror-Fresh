using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideShowCntrl : MonoBehaviour
{
    [SerializeField]
    GameObject imageOne;
    [SerializeField]
    GameObject imagetwo;
    [SerializeField]
    GameObject imagethree;
    public bool Has3Images = false;

    public sendMessage FinalMessage;

    float time = 0;
    public float fadeTime = 3f; 

    // Update is called once per frame
    private void Awake()
    {
        LeanTween.alpha(imageOne, 0f, fadeTime).setOnComplete(LoadImage2);
    }
    public void LoadImage2()
    {
        imageOne.SetActive(false);
        imagetwo.SetActive(true);
        if(Has3Images && imagethree != null)
        {
            LeanTween.alpha(imagetwo, 0f, fadeTime).setOnComplete(LoadImage3);
        }
        else
        {
            LeanTween.alpha(imagetwo, 0f, fadeTime).setOnComplete(Ending);
        }
    }

    public void LoadImage3()
    {
        
        imagetwo.SetActive(false);
        imagethree.SetActive(true);
        LeanTween.alpha(imagetwo, 0f, fadeTime).setOnComplete(Ending);
        
    }


    public void Ending()
    {
        FinalMessage.gameObject.SetActive(true);
        imagetwo.SetActive(false);
        LeanTween.alpha(imagetwo.gameObject, 0f, fadeTime).setOnComplete(FadeOutPanel);
        //imagetwo.SetActive(true);
    }
    public void FadeOutPanel()
    {
        this.gameObject.SetActive(false);
    }


    /*    void Update()
        {
            time += Time.deltaTime;

            if(time > 3)
            {
                imageone.SetActive(false);
                imagetwo.SetActive(true);
            }
            if(time > 6)
            {
                imagetwo.SetActive(false);
                imagethree.SetActive(true);

            }
            if(time > 8)
            {
                imagethree.SetActive(false);

            }
            if (time > 10)
            {
               // Destroy(this.gameObject);
               // this.gameObject.SetActive(false); 
            }
        }*/
}
