using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssingAudioManager : MonoBehaviour
{
    volumeHolder VolumeHolder;
    // Start is called before the first frame update
    private void Awake()
    {
        VolumeHolder = GameObject.Find("Volume holder").GetComponent<volumeHolder>();
    }
    private void OnEnable()
    {
        Slider.SliderEvent method = this.GetComponent<Slider>().onValueChanged;
      /*  Debug.Log(method.GetPersistentMethodName(0));
        Debug.Log(method.GetPersistentTarget(0));*/
        if(method.GetPersistentTarget(0) == null)
        {

        }
    }
}
