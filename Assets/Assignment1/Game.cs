using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game game;
    public Sprite player, AI;

    public GameObject topLeft;
    public GameObject topCenter;
    public GameObject topRight;
    public GameObject middleLeft;
    public GameObject middleCenter;
    public GameObject middleRight;
    public GameObject bottomLeft;
    public GameObject bottomCenter;
    public GameObject bottomRight;

    private GameObject[] top;
    private GameObject[] middle;
    private GameObject[] bottom;

    private int turnCounter;
    private int turn;

    List<GameObject> selection = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        game = this;
        RestartGame();
        
    }

    public void AIMove()
    {
        if (turn == 1) turnCounter++;
        
        switch (turnCounter)
        {
            case 0:
                selection.Add(topLeft);
                selection.Add(topCenter);
                selection.Add(topRight);
                selection.Add(middleLeft);
                selection.Add(middleRight);
                selection.Add(bottomLeft);
                selection.Add(bottomCenter);
                selection.Add(bottomRight);


                break;
            case 1:
                selection.Add(topLeft);
                selection.Add(topCenter);
                selection.Add(topRight);
                selection.Add(middleLeft);
                selection.Add(middleRight);
                selection.Add(bottomLeft);
                selection.Add(bottomCenter);
                selection.Add(bottomRight);
                if (topLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (topRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (bottomLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (bottomRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                //selection[Random.RandomRange(0, selection.Count - 1)].GetComponent<ButtonCall>().ChangeTile(0);
                break;
            case 2:
                selection.Add(topLeft);
                selection.Add(topRight);
                selection.Add(bottomLeft);
                selection.Add(bottomRight);
                if (topLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (topRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (bottomLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                if (bottomRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
                //selection[Random.RandomRange(0, selection.Count - 1)].GetComponent<ButtonCall>().ChangeTile(0);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }


        bool played = false;
        while (!played)
        {
            if(!selection[Random.RandomRange(0, selection.Count - 1)].GetComponent<ButtonCall>().IsUsed())
            {
                selection[Random.RandomRange(0, selection.Count-1)].GetComponent<ButtonCall>().ChangeTile(0);
                played = true;
            }
                        
        }

        if (turn == 0) turnCounter++;
    }

    public void CatchLastMove()
    {
        List<GameObject> catches = new List<GameObject>();
        if((topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && topCenter.GetComponent<ButtonCall>().PlayedBy() == 0) ||
           (middleRight.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(topRight);
        }

        if((topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(topCenter);
        }

        if((topCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(topLeft);
        }

        if((middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (topRight.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(middleRight);
        }

        if((middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 0)||
           (topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
           (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(middleCenter);
        }

        if((middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(middleLeft);
        }

        if((bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (middleRight.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(bottomRight);
        }

        if((bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && topCenter.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(bottomCenter);
        }

        if((bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
            (topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0))
        {
            catches.Add(bottomLeft);
        }

        if(catches.Count != 0)
        {
            selection = catches;
        }
    }

    public void CheckWin()
    {

    }

    public void RestartGame()
    {
        turnCounter = 0;
        topLeft.GetComponent<Image>().sprite = null;
        topCenter.GetComponent<Image>().sprite = null;
        topRight.GetComponent<Image>().sprite = null;
        middleLeft.GetComponent<Image>().sprite = null;
        middleCenter.GetComponent<Image>().sprite = null;
        middleRight.GetComponent<Image>().sprite = null;
        bottomLeft.GetComponent<Image>().sprite = null;
        bottomCenter.GetComponent<Image>().sprite = null;
        bottomRight.GetComponent<Image>().sprite = null;

        topLeft.GetComponent<ButtonCall>().Reset();
        topCenter.GetComponent<ButtonCall>().Reset();
        topRight.GetComponent<ButtonCall>().Reset();
        middleLeft.GetComponent<ButtonCall>().Reset();
        middleCenter.GetComponent<ButtonCall>().Reset();
        middleRight.GetComponent<ButtonCall>().Reset();
        bottomLeft.GetComponent<ButtonCall>().Reset();
        bottomCenter.GetComponent<ButtonCall>().Reset();
        bottomRight.GetComponent<ButtonCall>().Reset();
        bottomRight.GetComponent<ButtonCall>().Reset();

        float turn = Random.Range(0, 1);
        if(turn < 0.5f)
        {
            turn = 0;
            AIMove();
        }
        else
        {
            turn = 1;
        }
    }

    public Sprite player_Icon() { return player; }
    public Sprite AI_Icon() { return AI; }
}
