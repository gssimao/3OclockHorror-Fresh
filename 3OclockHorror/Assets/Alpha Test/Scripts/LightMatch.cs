using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightMatch : MonoBehaviour
{

    public SpriteMask lightMask;
    public GameObject match;
    [SerializeField]
    private SanityManager sanityManager;
    public float leanTime = 1;
    // the game will deduct .1 light at every hit by the monsters accumulating a total of 5.5 hits per game before gameover
    //at every hit the sanity should decrease by 18.18 of the 100 total
    Vector3 currentMaskSize = new Vector3(0.85f, 0.85f, 0);
    AudioManager manager;

    UniversalControls uControls;

    private void Awake()
    {
        sanityManager = GameObject.Find("Player2").GetComponent<SanityManager>();
        uControls = new UniversalControls();
        uControls.Enable();
        uControls.Player.Light.performed += LosingSanity;
    }

    private void OnDisable()
    {
        uControls.Disable();
    }

    void Start()
    {
        lightMask.transform.localScale = currentMaskSize;
        manager = FindObjectOfType<AudioManager>();
    }

    private void LosingSanity(InputAction.CallbackContext context)
    {
        ChangeMaskSize(-.1f);
    }
    public void TurnOffLight(bool toggle)
    {
        match.gameObject.SetActive(toggle);
    }
    public void ChangeMaskSize(float sizeChange) // a positive number will increase the size, a negative number will dicrease size
    {
        currentMaskSize = new Vector3((currentMaskSize.x + sizeChange), (currentMaskSize.y + sizeChange), 0);
        AjustMaskSize(currentMaskSize);
    }
    public void AjustMaskSize(Vector3 size)
    {
        lightMask.transform.LeanScale(currentMaskSize, 0.3f);
    }
}
