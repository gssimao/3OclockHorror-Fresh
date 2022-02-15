using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour
{
    [SerializeField]
    List<Item> Photos;

    [SerializeField]
    List<LPhotoCntrl> LPhotos;


    int dateSelector;
    List<string> SelectedDate;

    List<string> Dates1 = new List<string> { "1896", "1897", "1899", "1903" };
    List<string> Dates2 = new List<string> { "1892", "1894", "1899", "1900" };
    List<string> Dates3 = new List<string> { "1891", "1898", "1905", "1907" };
    List<string> Numerals = new List<string> { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };


    [SerializeField]
    Padlock padlock;

    private void Start()
    {
        if(Photos == null)
        {
            Debug.LogError("One or more of the necessary items for photo puzzle initiation is not set.");
        }

        if(LPhotos.Count != 4)
        {
            Debug.LogError("Need 4 LPhotos");
        }

        dateSelector = Random.Range(0, 3);

        if(dateSelector == 0)
        {
            SelectedDate = Dates1;
        }
        else if(dateSelector == 1)
        {
            SelectedDate = Dates2;
        }
        else
        {
            SelectedDate = Dates3;
        }


        DistPhotos();

    }

    public void DistPhotos()
    {
        for(int i = 0; i < 4; i++)
        {
            Debug.Log("calling distPhotos " + i + " times");
            int rand = Random.Range(0, Photos.Count);
            Item photo = Photos[rand];
            rand = Random.Range(0, 3);

            Photos.Remove(photo);

            rand = Random.Range(0, Numerals.Count);
            photo.numeral = Numerals[rand];
            Numerals.RemoveAt(rand);

            photo.date = SelectedDate[i];

            LPhotos[i].InitLargePhoto(photo);


            if (i == 0)
            {

                padlock.Photo1 = photo;
            }
            else if (i == 1)
            {

                padlock.Photo2 = photo;
            }
            else if(i == 2)
            {

                padlock.Photo3 = photo;
            }
            else if(i == 3)
            {

                padlock.Photo4 = photo;
            }

           // photoRecipie.Pieces.Add(photo);
        }


    }

    public void InitPuzzle()
    {

        int rand = Random.Range(0, Photos.Count);
        Item photo = Photos[rand];
        Photos.Remove(photo);
        padlock.Photo1 = photo;
        Debug.Log("set photo 1");

        rand = Random.Range(0, Numerals.Count);
        photo.numeral = Numerals[rand];
        Numerals.RemoveAt(rand);
        photo.date = SelectedDate[0];

        LPhotos[0].InitLargePhoto(photo);

        DistPhotos();
    }
}
