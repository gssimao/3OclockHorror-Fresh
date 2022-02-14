using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPuzzle : MonoBehaviour // no longer needed
{

    /*    public int photoFoundCount = 0;*/
    /*    [SerializeField]
        Inventory plyInv;
        [SerializeField]
        GameObject invCanv;*/

    /*    [SerializeField]
        GameObject puzzleGO;*/
    [SerializeField]
    List<LPhotoCntrl> photos;


    // Start is called before the first frame update
    /*   void Start()
       {
           for (int i = 0; i < photos.Count; i++)
           {
               photos[i].gameObject.SetActive(false);
           }
       }*/

    // Update is called once per frame

    public void StartPhotoPuzzle()
    {
        for (int i = 0; i < photos.Count; i++)
        {
            //invCanv.SetActive(true);
            //photoFoundCount++;

            //plyInv.RemoveItem(photos[i].myPhoto); // remove the item from the player inventory
            photos[i].gameObject.SetActive(true);
            GameObject photoPuzzle = GameObject.Find("Photo Frame");
            photoPuzzle.GetComponent<PuzzleOpenerScript>().havePhoto = true;

            photos.RemoveAt(i);

        }
    }
 /*   void Update()
    {
        if (puzzleGO.activeSelf)
        {
            for (int i = 0; i < photos.Count; i++)
            {
                if (!plyInv.ContainsItem(photos[i].myPhoto) || photos[i].myPhoto == null)
                {
                    photos[i].gameObject.SetActive(false);
                }
                else
                {
                    invCanv.SetActive(true);
                    photoFoundCount++;
                    photos[i].gameObject.SetActive(true);
                    plyInv.RemoveItem(photos[i].myPhoto); // remove the item from the player inventory
                    openWithNoPhotos = false;
                    GameObject photoPuzzle = GameObject.Find("Photo Frame");
                    photoPuzzle.GetComponent<PuzzleOpenerScript>().havePhoto = true;
                    if(photoFoundCount == 4)
                    {
                        photoPuzzle.GetComponent<PuzzleOpenerScript>().allPhotoFounds = true;
                    }
                    //Debug.Log("Item: " + photos[i].myPhoto.ItemName + " at index: " + i + " has been removed");
                    photos.RemoveAt(i);

                    invCanv.SetActive(false);
                }
            }
        }
    }*/


}
