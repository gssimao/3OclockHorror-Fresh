using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NoteGO : MonoBehaviour
{
    public Sprite sprite;
    public Image image;
    public GameObject NoteText;
    public Sprite transparent;
    //public static Action<GameObject> UpdateInventoryGO = delegate { };
    public static Action<GameObject, Image, Sprite, Sprite> SetUpNote = delegate { };

    private void Start()
    {
        SetUpNote(NoteText, image, sprite, transparent);
    }
}
