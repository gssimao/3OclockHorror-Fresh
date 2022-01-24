using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class GameActionTrigger : MonoBehaviour
{
    public List<GameActions> defaultAction;   
    public bool bAutoTrigger;
    private UniversalControls uControls;

    public static Action TriggerActive = delegate { };
    public static Action TriggerInactive = delegate { };

    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerActive();
        if (bAutoTrigger)
            Execute();
        else
            uControls.Player.Interact.started += Execute; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerInactive();
        if (!bAutoTrigger)
            uControls.Player.Interact.started -= Execute;
    }
    private void Execute(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(TriggerAction));
    }
    public void Execute()
    {
        StartCoroutine(nameof(TriggerAction));
    }
    IEnumerator TriggerAction()
    {
        for (int x = 0; x < defaultAction.Count; x++)
        {
            yield return new WaitForSeconds(defaultAction[x].delay);
            defaultAction[x].Action();
        }
    }
}
