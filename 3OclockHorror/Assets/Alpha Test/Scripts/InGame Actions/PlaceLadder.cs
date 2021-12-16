using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceLadder : MonoBehaviour
{
    [SerializeField]
    Inventory plyInv;
    [SerializeField]
    PlayerMovement player;
    [SerializeField]
    Item Ladder;
    [SerializeField]
    GameObject invCanvas;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    GameObject destination;
    [SerializeField]
    room desRoom;

    [Space]
    [SerializeField]
    SpriteRenderer Room;
    [SerializeField]
    Sprite newRoom;
    private Image BlackBackground;
    private float transitionTime = 0.5f;
    private bool ladAcquired = false;

    float dist;

    [SerializeField]
    string destString;

    UniversalControls uControls;
    private void Awake()
    {
        BlackBackground = GameObject.Find("TransitionPanel").GetComponent<Image>();
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
   
    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= 0.5f)
        {
            if (ladAcquired)
            {
                if (uControls.Player.Interact.triggered)
                {
                    LadderFunction();
                }
            }
            else if (!ladAcquired)
            {
                if (uControls.Player.Interact.triggered)
                {
                    if (plyInv.ContainsItem(Ladder))
                    {
                        invCanvas.SetActive(true);
                        plyInv.RemoveItem(Ladder);
                        invCanvas.SetActive(false);
                        Room.sprite = newRoom;
                        ladAcquired = true;

                    }
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }

    void LadderFunction()
    {
        StartCoroutine(Transition());
    }


    IEnumerator Transition()
    {

        player.myRoom = desRoom;
        player.playerFloor = destString;

        LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).setEaseInBack().setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setOnComplete(() =>
        {
            ChangePlayerPosition(player.gameObject, destination.gameObject);
        });

        yield return new WaitForSeconds(transitionTime * 2);
        LeanTween.value(BlackBackground.gameObject, 1, 0, transitionTime * 2).setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setEaseInBack();
        
       

    }
    private void ChangePlayerPosition(GameObject playerObject, GameObject entranceP)
    {
        playerObject.transform.position = entranceP.transform.position; // take player to the new place

    }
}
