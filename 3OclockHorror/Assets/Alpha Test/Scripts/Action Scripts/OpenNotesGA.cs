using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public  class OpenNotesGA : GameActions
{
    public Sprite sprite;
    [SerializeField] private Image image;
    [TextArea(1,20)] public string Note;
    [SerializeField] private GameObject NoteText;
    [SerializeField] private Sprite transparent;
    [SerializeField]public bool active = false;
    public override void Action()
    {
        Note = this.gameObject.GetComponent<ObjectRender>().Note;
        Debug.Log("getting notes");
        if (!active)
        {
            active = true;
            image.sprite = sprite;
            NoteText.SetActive(true);
            NoteText.GetComponent<Text>().text = Note;
            Time.timeScale = 0;
            Debug.Log("OPEN notes");
        }
        else
        {
            NoteText.GetComponent<Text>().text = "";
            NoteText.SetActive(false);
            image.sprite = transparent;
            active = false;
            Time.timeScale = 1;
            Debug.Log("CLOSE notes");
        }
    }
}
