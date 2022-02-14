using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PuzzleOpenerGA : GameActions 
{
    private int messageCounter = 0;
    public List<sendMessage> SendMessageList;
    

    [SerializeField]
    GameObject Puzzle; // where the puzzle is being held
    public List<Item> NecesseryItems;
    public bool SolvedPuzzle;
    [SerializeField] ContainerItems containerItems; // This should be referencing the player's container

    public override void Action()
    {
        if(SolvedPuzzle)
        {
            Puzzle.SetActive(true);
            return;
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

            if (SendMessageList.Count-1 > messageCounter)
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
