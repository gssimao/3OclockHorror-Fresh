using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscCntrl : MonoBehaviour
{
    [SerializeField]
    Animator Fade;
    Scene currentScene;
    AudioManager manager;
    private bool endingTriggered = false;
    public sendMessage CantScapeMessage;
    private void Awake()
    {
        manager = FindObjectOfType<AudioManager>();
    }
    public void ReloadGame()
    {
        if (!endingTriggered)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        else
        {
            this.gameObject.SetActive(false);
            CantScapeMessage.TriggerCreepyFont();
        }
    }

    public void MainMenu()
    {
<<<<<<< Updated upstream
        manager = FindObjectOfType<AudioManager>();
        manager.StopAll();
        Debug.Log("Main menu time");
        ChangeScene();
=======
        if(!endingTriggered)
        {
            manager.StopAll();
            ChangeScene();
        }
        else
        {
            this.gameObject.SetActive(false);
            CantScapeMessage.TriggerCreepyFont();
        }

>>>>>>> Stashed changes
    }
    public void ChangeScene()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public IEnumerator LoadYourAsyncScene()
    {
        Fade.gameObject.SetActive(true);
        Fade.SetTrigger("fadeOut");
        

        currentScene = SceneManager.GetActiveScene();

        //yield return new WaitForSeconds(0.7f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_Menu_Alpha_(options)", LoadSceneMode.Additive);

        while (!asyncLoad.isDone && Fade.GetComponent<CanvasGroup>().alpha != 1)// Runs this code until the next scene is done loading
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
    }
    public void EndingComplete(bool complete)
    {
        endingTriggered = complete;
    }
}