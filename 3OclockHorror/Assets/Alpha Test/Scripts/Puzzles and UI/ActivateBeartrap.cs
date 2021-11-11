using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBeartrap : MonoBehaviour
{
    public HuntCheckSolved AnswerCheck;
    public GameObject Puzzle;

    private bool CanTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanTrigger)
        {
            AnswerCheck.GetComponent<HuntCheckSolved>().Activate(Puzzle, this.gameObject);
            AnswerCheck.RestartingPuzzle();
        }
        
    }
    public void ChangeTrigger(bool newValue, Sprite newSprite)
    {
        CanTrigger = newValue;
        this.GetComponent<SpriteRenderer>().sprite = newSprite;

    }
}
