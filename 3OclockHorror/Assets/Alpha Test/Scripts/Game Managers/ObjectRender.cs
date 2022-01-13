using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRender : MonoBehaviour
{
    public Sprite sprite;
    [SerializeField] private Image image;
    [TextArea] public string Note;
    [SerializeField] private GameObject NoteText;
    [SerializeField] private Sprite transparent;
    [SerializeField]
    public bool active= false;
    public invInput Listener;


    UniversalControls uControls;
    void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
        active = false;
        image = GameObject.Find("PopupImage").GetComponent<Image>();
        NoteText = GameObject.Find("NoteText");
        NoteText.GetComponent<Text>().text = "";
        transparent = image.sprite;
    }

  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        Listener.NoteSwitch(true);
        OpenNotes.TriggerNote += NotesOpenClose;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Listener.NoteSwitch(false);
        OpenNotes.TriggerNote -= NotesOpenClose;
    }*/


    private void OnDisable()
    {
        uControls.Disable();
    }
    public void NotesOpenClose()
    {
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
