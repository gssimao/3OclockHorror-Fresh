using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkbenchGO : MonoBehaviour
{
    public GameObject workbenchCanvas, workbenchTooltip, Player;
    public static Action<GameObject> UpdateInventoryGO = delegate { };
    public static Action<GameObject, GameObject, GameObject> UpdateWorkbenchDisplay = delegate { };

    private void Start()
    {
        //UpdateInventoryGO(workbenchSlot);
        UpdateWorkbenchDisplay(workbenchCanvas, workbenchTooltip, Player);
    }
}

