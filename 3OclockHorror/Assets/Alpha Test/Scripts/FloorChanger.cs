using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloorChanger : MonoBehaviour
{
    private float transitionTime = 0.5f;
    public string sceneName; //name of the scene to transfer too
    //Scene currentScene;
    public GameObject spawnPoint;
    public GameObject player;
    public invInput Listener;
    public room destRoom;

    public Animator Fade;
    private Image BlackBackground;

    [SerializeField]
    string destString;

    float dist;
    bool animIsDone = false;

    AudioManager manager;

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
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (Mathf.Abs(dist) <= 0.6f)
        {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered)
            {
                StartCoroutine(ChangeCamera(player.gameObject));   
            }
        }

        if(Fade != null)
        {
            if(Fade.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Fade.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ChangeCamera(GameObject playerObject)
    {

        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<PlayerMovement>().myRoom = destRoom;
        player.GetComponent<PlayerMovement>().playerFloor = destString;

        LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).setEaseInBack().setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setOnComplete(() =>
        {
            ChangePlayerPosition(playerObject, destRoom.gameObject);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, 0.6f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPoint.transform.position, 0.1f);
        Vector3 plyPos = spawnPoint.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(plyPos.x, plyPos.y - 0.3108585f, plyPos.z), new Vector3(0.1573486f, 0.1247783f, 1f));
    }

    void playSound()
    {
        if (manager != null)
        {
            //manager.AudioFadeOut("Drone", 5);
            //manager.AudioFadeOut("Game ST", 5);
        }
    }


}
