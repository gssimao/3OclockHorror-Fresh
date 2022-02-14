using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallCraftDelegate : MonoBehaviour
{
    public static Action Craft = delegate { };
    
    public void CraftClick()
    {
        Craft();
    }

}
