using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumeHolder : MonoBehaviour
{
    //public AudioMixer audioMixer;
    public static volumeHolder instance;
    [SerializeField]
    float volume = 1;
    [SerializeField]
    AudioManager manager;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != null)
        {
            Debug.Log(instance);
            Destroy(gameObject); //Is there a manager? If yes then I'm gone
            
        }
        else
        {
            instance = this;  //There isnt a manager? I'm it
            DontDestroyOnLoad(gameObject);
        }

       // setVolume();

    }

    public void OnSliderValueChanged(float value)
    {
        volume = value;
       setVolume();
    }

    public void setVolume()
    {
        manager = FindObjectOfType<AudioManager>();
        manager.updateVolume(volume);
    }
}
