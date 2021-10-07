using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    drawingPuzzle PuzzleCntrl;
    public List<GameObject> connections; //Updated when a line is drawn from here

    Image sr;

    //[HideInInspector]
    public bool isOn;
    public bool ISCAP;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(PuzzleCntrl.getSelected() == null)
            {
                PuzzleCntrl.setSelected(gameObject);
                PuzzleCntrl.setSR(sr);
            }
            else if(PuzzleCntrl.getSelected() != gameObject)
            {
                PuzzleCntrl.DrawLine(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + this.name + " in " + this.transform.parent.name);
        if (collision.tag == "RedBookLine")
        {
            isOn = true;
        }
    }
}
