using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGA : MonoBehaviour
{
    public List<GameActions> actions;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameActions item in actions)
            item.Action();
    }
}
