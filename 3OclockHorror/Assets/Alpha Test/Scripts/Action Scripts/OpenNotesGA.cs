using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public  class OpenNotesGA : GameActions
{
    [TextArea(1, 20)] public string Note;
    [Space]
    private Sprite sprite;
    private Image image;
    private GameObject NoteText;
    private Sprite transparent;
    public bool active = false;
    private void OnEnable()
    {
        NoteGO.SetUpNote += setUpNote;
    }
    private void OnDisable()
    {
        NoteGO.SetUpNote -= setUpNote;
    }
    private void setUpNote(GameObject noteText, Image img, Sprite sprt, Sprite transprt )
    {
        image = img;
        NoteText = noteText;
        sprite = sprt;
        transparent = transprt;
    }
    public override void Action()
    {
        Note = this.gameObject.GetComponent<ObjectRender>().Note;
        if (!active)
        {
            active = true;
            image.sprite = sprite;
            NoteText.SetActive(true);
            NoteText.GetComponent<Text>().text = Note;
            Time.timeScale = 0;
        }
        else
        {
            NoteText.GetComponent<Text>().text = "";
            NoteText.SetActive(false);
            image.sprite = transparent;
            active = false;
            Time.timeScale = 1;
        }
    }
}
