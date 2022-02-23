using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PuzzleOpenerGA : GameActions 
{
    private int messageCounter = 0;
    public List<sendMessage> SendMessageList;

    public static Action<int> ContainerRequest = delegate { };

    [SerializeField]
    GameObject Puzzle; // where the puzzle is being held
    public List<Item> NecesseryItems;
    public bool SolvedPuzzle;
    [SerializeField] ContainerItems containerItems; // This should be referencing the player's container
    private bool bRecieved;

    private void OnEnable()
    {
        ContainerItems.ReceiveContainer += ContainerCollector;
    }
    private void OnDisable()
    {
        ContainerItems.ReceiveContainer -= ContainerCollector;
    }
    public override void Action()
    {
        StartCoroutine(nameof(RecieveContainer));

    }
    private void ContainerCollector(ContainerItems v)
    {
        bRecieved = true;
        if (v)
            containerItems = v;
        else
            containerItems = null;
    }
    IEnumerator RecieveContainer()
    {
        if (SolvedPuzzle)
        {
            Puzzle.SetActive(true);
            yield break;
        }

        ContainerRequest(1); // sending ID

        while (!bRecieved) //waiting for response from ContainerItems with the correct ID
            yield return new WaitForEndOfFrame();

        if(containerItems == null)
        {
            Debug.LogError("No container received.");
            yield break;
        }
        if (containerItems.ContainsItem(NecesseryItems)) // check if the player has the items needed to open the puzzle
        {
            SolvedPuzzle = true;
            removeItems();
            Puzzle.SetActive(true);
        }
        else // player does NOT have the items needed send him a message/hint about this puzzle
        {
            SendMessageList[messageCounter].TriggerMessage(); // if there is no more hint messages the last message will be replaying.

            if (SendMessageList.Count - 1 > messageCounter)
            {
                messageCounter++;
            }
        }
        
    }
    private void removeItems()
    {
        foreach(Item item in NecesseryItems)
        {
            containerItems.RemoveItem(item);
        }
    }

}
