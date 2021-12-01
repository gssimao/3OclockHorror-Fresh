using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheHouse : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    HuntCheckSolved EntrancePuzzle;
    [SerializeField]
    public Inventory plyinv;
    Scene currentScene;
    [SerializeField]
    string sceneName; // name of scene to transfer to
    [SerializeField]
    Animator Fade;
    [SerializeField]
    invInput Listener;
    [SerializeField]
    public Item key;
    [SerializeField]
    GameObject BTPuzzle;
    [SerializeField]
    GameObject KeyPopUp;
    AudioManager manager;
    public sendMessage hintMessage;
    public sendMessage hint2Message;
    private bool lockSolved = false;

    public float dist;
    float range = 0.5f;

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
        dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= range)
        {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered) // interact with door
            {
                TryToEnter();
            }
        }
    }
    public void TryToEnter()
    {
        if (!GetlockSolved() && !plyinv.ContainsItem(key)) // not solved and no key show hint1
        {
            BTPuzzle.SetActive(true);
            if (hintMessage != null)
                hintMessage.TriggerMessage();
        }
        else if (!GetlockSolved() && plyinv.ContainsItem(key)) // not solved but has key
        {
            BTPuzzle.SetActive(true);
        }
        else if (GetlockSolved() && !plyinv.ContainsItem(key)) // solved but no key, show hint2
        {
            hint2Message.TriggerMessage();
            if (KeyPopUp != null) // show image once
            {
                KeyPopUp.SetActive(true);
            }

        }
        else if (GetlockSolved() && plyinv.ContainsItem(key))
        {
            //calling house
            Enterthehouse();
        }
    }
    private bool GetlockSolved()
    {
        if(!lockSolved)
            lockSolved = EntrancePuzzle.solved;
        return lockSolved;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    public void Enterthehouse()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public IEnumerator LoadYourAsyncScene()
    {
        Fade.gameObject.SetActive(true);
        Fade.SetTrigger("fadeOut");

        currentScene = SceneManager.GetActiveScene();

        yield return new WaitForSeconds(Fade.GetCurrentAnimatorStateInfo(0).length);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)// Runs this code until the next scene is done loading
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
    }
}
