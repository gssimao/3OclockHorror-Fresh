using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameActionTrigger : MonoBehaviour
{
    public List<GameActions> defaultAction;   
    public bool bAutoTrigger;
    private UniversalControls uControls;

    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bAutoTrigger)
            Execute();
        else
            uControls.Player.Interact.performed += Execute; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!bAutoTrigger)
            uControls.Player.Interact.performed -= Execute;
    }
    private void Execute(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(TriggerAction));
    }
    private void Execute()
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
