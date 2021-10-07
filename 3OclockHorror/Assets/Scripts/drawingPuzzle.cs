﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawingPuzzle : MonoBehaviour
{
    GameObject selected = null; //Storage for the selected point and it's image renderer
    Image sr = null;

    [SerializeField]
    DrawingHolder mainBoard;
    [Space]
    [SerializeField]
    DrawingHolder answerOne;
    [SerializeField]
    DrawingHolder answerTwo;
    [SerializeField]
    DrawingHolder answerThree;
    [SerializeField]
    DrawingHolder answerFour;
    [SerializeField]
    DrawingHolder anxBoard;
    [SerializeField]
    AnxEffect anxiety;
    [SerializeField]
    DrawingHolder abanBoard;
    [SerializeField]
    AbandonmentEffect abnEffect;
    [SerializeField]
    DrawingHolder profBoard;
    [SerializeField]
    profaneEffect profEffect;

    [Space]
    [SerializeField]
    GameObject LineParent; //The storage for all lines
    [SerializeField]
    GameObject lineConnection; //The actual prefab line

    [Space]
    [SerializeField]
    symbolUpdater sym1;
    [SerializeField]
    symbolUpdater sym2;
    [SerializeField]
    symbolUpdater sym3;
    [SerializeField]
    symbolUpdater sym4;

    [Space]
    [SerializeField]
    TaskListTracker taskList;
    bool msgSent = false;

    AudioManager manager;

    bool symOne = false;
    bool symTwo = false;
    bool symThree = false;
    bool symFour = false;
    bool anx = false;
    bool abn = false;
    bool prof = false;

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

    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uControls.UI.OtherSelect.triggered/*Input.GetMouseButtonDown(1)*/ && selected != null) //If the mouse is right clicked and there is a selected point, deselect it
        {
            resetSelection();
        }

        if (sr != null)
        {
            sr.color = Color.red; //Show the selected point, if any, in a special color.
        }
    }

    #region Selected Get/Set
    public GameObject getSelected()
    {
        return selected;
    }
    public void setSelected(GameObject newObj)
    {
        selected = newObj;
    }
    public void setSR(Image newImg)
    {
        sr = newImg;
    }
    #endregion

    #region Drawing Lines
    //Draw a line when one point is selected and another is clicked
    public void DrawLine(GameObject obj2)
    {
        /*
        LineRenderer lr = LineParent.AddComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();
        pos.Add(genVec3Pos(selected));
        pos.Add(genVec3Pos(obj2));
        lr.startWidth = 1f;
        lr.endWidth = 1f;
        lr.SetPositions(pos.ToArray());
        lr.useWorldSpace = true;
        lr.renderingLayerMask = 9;
        */
        DrawPoint selecteddp = selected.GetComponent<DrawPoint>();
        DrawPoint obj2dp = obj2.GetComponent<DrawPoint>();

        if (selecteddp != null && obj2dp != null)
        {
            if (!selecteddp.connections.Contains(obj2))
            {
                createALine(selected, obj2);
                selected.GetComponent<DrawPoint>().connections.Add(obj2);
                obj2.GetComponent<DrawPoint>().connections.Add(selected);

                selecteddp.isOn = true;
                obj2dp.isOn = true;
            }
            else
            {
                //any code to fire if the connection already exists
            }
        }
        resetSelection();
    }

    //Grab the line prefab and create a long / thin one between two points
    private void createALine(GameObject objA, GameObject objB)
    {
        /*spawn a prefab image "lineConnection" as angleBar*/
        GameObject angleBar = Instantiate(lineConnection, objB.transform.position, Quaternion.identity);
        /**/
        /*calculate angle*/
        Vector2 diference = objA.transform.position - objB.transform.position;
        float sign = (objA.transform.position.y < objB.transform.position.y) ? -1.0f : 1.0f;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;
        angleBar.transform.Rotate(0, 0, angle);
        /**/
        /*calculate length of bar*/
        float height = 5;
        float width = Vector2.Distance(objB.transform.position, objA.transform.position);
        angleBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        /**/
        /*calculate midpoint position*/
        float newposX = objB.transform.position.x + (objA.transform.position.x - objB.transform.position.x) / 2;
        float newposY = objB.transform.position.y + (objA.transform.position.y - objB.transform.position.y) / 2;
        angleBar.transform.position = new Vector3(newposX, newposY, 0);
        /***/
        /*set parent to Line Parent*/
        angleBar.transform.SetParent(LineParent.transform, true);
    }

    /*
    public Vector3 genVec3Pos(GameObject obj)
    {
        return new Vector3(obj.transform.position.x, obj.transform.position.y, -0.01f);
    }
    */

    //Reset the selected point's image, so that the point isn't selected and it's image isn't red anymore.
    public void resetSelection()
    {
        selected = null;
        if (sr != null)
        {
            sr.color = Color.white;
            sr = null;
        }
    }
    #endregion

    //Resets the lines
    public void Reset()
    {
        foreach (Transform child in LineParent.transform)
        {
            Destroy(child.gameObject);
        }
        mainBoard.ClearConnections();
        resetSelection();
    }
    //Checks the answer
    public void checkAnswer()
    {
        //Check through each board in reference to the current board - and if one is found, that would trigger the functions for the door and clear the board
        bool check = false;
        bool passed = false;

        if (!symOne)
        {
            check = compareBoards(mainBoard, answerOne);
            if (check)
            {
                symOne = true;
                sym1.UpdateSprite();

                Debug.Log("Correct");

                passed = true;
            }
        }
        if (!symTwo)
        {
            check = compareBoards(mainBoard, answerTwo);
            if (check)
            {
                symTwo = true;
                sym2.UpdateSprite();

                Debug.Log("Correct");

                passed = true;
            }
        }
        if (!symThree)
        {
            check = compareBoards(mainBoard, answerThree);
            if (check)
            {
                symThree = true;
                sym3.UpdateSprite();

                Debug.Log("Correct");

                passed = true;
            }
        }
        if (!symFour)
        {
            check = compareBoards(mainBoard, answerFour);
            if (check)
            {
                symFour = true;
                sym4.UpdateSprite();

                Debug.Log("Correct");

                passed = true;
            }
        }
        if (!anx)
        {
            check = compareBoards(mainBoard, anxBoard);
            if (check)
            {
                anxiety.activateAnxiety();
                passed = true;
            }
        }
        if (!abn)
        {
            check = compareBoards(mainBoard, abanBoard);
            if (check)
            {
                abnEffect.Activate = true;
                passed = true;
            }
        }
        if (!prof)
        {
            check = compareBoards(mainBoard, profBoard);
            if (check)
            {
                profEffect.activateProfane();
                passed = true;
            }
        }

        if (passed)
        {
            manager.Play("Book success", true);

            if(msgSent == false)
            {
                taskList.updateList("\n - When I drew that symbol, one of the lights turned on. I wonder what happens when all four are lit?");
                msgSent = true;
            }

            Reset();
        }
        else
        {
            manager.Play("Book fail", true);
        }
    }

    public bool compareBoards(DrawingHolder main, DrawingHolder answer)
    {
        //Left left
        int i = 0;
        foreach (DrawPoint point in main.leftLeft)
        {
            if (point.isOn != answer.leftLeft[i].isOn)
            {
                return false;
            }
            if (answer.leftLeft[i].ISCAP && point.connections.Count == 0)
            {
                return false;
            }
            i++;
        }

        i = 0;
        foreach (DrawPoint point in main.midLeft)
        {
            if (point.isOn != answer.midLeft[i].isOn)
            {
                return false;
            }
            if (answer.midLeft[i].ISCAP && point.connections.Count == 0)
            {
                return false;
            }
            i++;
        }

        i = 0;
        foreach (DrawPoint point in main.mid)
        {
            if (point.isOn != answer.mid[i].isOn)
            {
                return false;
            }
            if (answer.mid[i].ISCAP && point.connections.Count == 0)
            {
                return false;
            }
            i++;
        }

        i = 0;
        foreach (DrawPoint point in main.midRight)
        {
            if (point.isOn != answer.midRight[i].isOn)
            {
                return false;
            }
            if (answer.midRight[i].ISCAP && point.connections.Count == 0)
            {
                return false;
            }
            i++;
        }

        i = 0;
        foreach (DrawPoint point in main.rightRight)
        {
            if (point.isOn != answer.rightRight[i].isOn)
            {
                return false;
            }
            if (answer.rightRight[i].ISCAP && point.connections.Count == 0)
            {
                return false;
            }
            i++;
        }

        return true;
    }
}
