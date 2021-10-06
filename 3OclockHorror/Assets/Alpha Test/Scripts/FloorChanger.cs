﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChanger : MonoBehaviour
{
    public string sceneName; //name of the scene to transfer too
    Scene currentScene;
    public GameObject spawnPoint;
    public GameObject player;
    public invInput Listener;
    public room destRoom;

    public Animator Fade;

    [SerializeField]
    string destString;

    float dist;
    bool animIsDone = false;

    AudioManager manager;

    UniversalControls uControls;
    private void Awake()
    {
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
                StartCoroutine(ChangeCamera());   
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

    IEnumerator ChangeCamera()
    {
        Fade.gameObject.SetActive(true);
        Fade.SetTrigger("fadeOut");

        yield return new WaitForSeconds(0.5f);

        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<PlayerMovement>().myRoom = destRoom;
        player.GetComponent<PlayerMovement>().playerFloor = destString;
        //playSound();
        Fade.SetTrigger("fadeIn");
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

    /*IEnumerator LoadYourAsyncScene(GameObject Instance)
    {
        currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        player.transform.position = spawnPoint.transform.position;
        blackWall.SetActive(false);
        crossFade.SetActive(false);

        SceneManager.MoveGameObjectToScene(Instance, SceneManager.GetSceneByName(sceneName));

        SceneManager.UnloadSceneAsync(currentScene);
    }*/
}
