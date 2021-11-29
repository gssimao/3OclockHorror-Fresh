using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableForAndroid : MonoBehaviour
{
    private void Awake() //turn off objects if this is not an Android build 
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            this.gameObject.SetActive(false);
        }
    }
}
