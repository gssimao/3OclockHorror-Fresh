using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableforWindows : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            this.gameObject.SetActive(false);
        }
    }
}
