using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public static Game game;
    public Sprite player, AI;
    public TextMeshProUGUI victoryText;

    public GameObject topLeft;
    public GameObject topCenter;
    public GameObject topRight;
    public GameObject middleLeft;
    public GameObject middleCenter;
    public GameObject middleRight;
    public GameObject bottomLeft;
    public GameObject bottomCenter;
    public GameObject bottomRight;

    private int turnCounter;
    private int turn;

    ButtonCall[,] board = new ButtonCall[3,3];
    //List<GameObject> selection = new List<GameObject>();
        //0 = AI, 1 = player

    void Start()
    {
        game = this;
       
        board[0, 0] = topLeft.GetComponent<ButtonCall>();
        board[0, 1] = topCenter.GetComponent<ButtonCall>();
        board[0, 2] = topRight.GetComponent<ButtonCall>();
        board[1, 0] = middleLeft.GetComponent<ButtonCall>();
        board[1, 1] = middleCenter.GetComponent<ButtonCall>();
        board[1, 2] = middleRight.GetComponent<ButtonCall>();
        board[2, 0] = bottomLeft.GetComponent<ButtonCall>();
        board[2, 1] = bottomCenter.GetComponent<ButtonCall>();
        board[2, 2] = bottomRight.GetComponent<ButtonCall>();

        //RestartGame();
    }

    public void AIMove()
    {
        int bestVal = -1000;
        int rowPos = 0;
        int colPos = 0;

        if (MovesRemaining())
        {
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    if(board[row, col].PlayedBy() == -1)
                    {
                        board[row, col].ChangeTile(0);

                        int moveVal = MiniMax(0, false);

                        board[row, col].Reset();

                        if(moveVal > bestVal)
                        {
                            rowPos = row;
                            colPos = col;
                            bestVal = moveVal;
                        }
                    }
                }
            }
            board[rowPos, colPos].ChangeTile(0);
        }
        else
        {
            switch (VictoryCheck())
            {
                case -1:
                    victoryText.text = "Tie";
                    break;
                case 0:
                    victoryText.text = "AI wins";
                    break;
                case 1:
                    victoryText.text = "Player wins";
                    break;
            }
        }


        //if(bestVal == -1000)
        //{
        //    rowPos = Random.Range(0, 3);
        //    colPos = Random.Range(0, 3);
        //}
        //Debug.Log(rowPos.ToString() + " "+ colPos.ToString() +": " + bestVal);

    }

    int VictoryCheck()
    {
        for(int row = 0; row < 3; row++)
        {
            if(board[row, 0] == board[row, 1] &&
                board[row, 1] == board[row, 2])
            {
                if(board[row, 0].PlayedBy() == 1)
                {
                    //player win
                    return 1;
                }
                else if(board[row, 0].PlayedBy() == 0)
                {
                    //ai wins
                    return 0;
                }
            }
        }

        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] == board[1, col] &&
                board[1, col] == board[2, col])
            {
                if (board[0, col].PlayedBy() == 1)
                {
                    //player win
                    return 1;
                }
                else if (board[0, col].PlayedBy() == 0)
                {
                    //ai wins
                    return 0;
                }
            }
        }

        if(board[0, 0].PlayedBy() == board[1, 1].PlayedBy() && board[1, 1].PlayedBy() == board[2, 2].PlayedBy())
        {
            if(board[0, 0].PlayedBy() == 1)
            {
                //player Wins
                return 1;
            }
            else if (board[0, 0].PlayedBy() == 0)
            {
                //ai Wins
                return 0;
            }
        }

        if (board[0, 2].PlayedBy() == board[1, 1].PlayedBy() && board[1, 1].PlayedBy() == board[2, 0].PlayedBy())
        {
            if (board[0, 2].PlayedBy() == 1)
            {
                //player Wins
                return 1;
            }
            else if (board[0, 2].PlayedBy() == 0)
            {
                //ai Wins
                return 0;
            }
        }

        return -1;
    }

    int MiniMax(int depth, bool isMax)
    {
        int score = Evaluate();

        if (score == 10)
            return score;
        if (score == -10)
            return score;

        if (MovesRemaining() == false)
            return 0;

        if (isMax)
        {
            int best = -1000;

            for (int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    if(board[x, y].PlayedBy() == -1)
                    {
                        board[x, y].ChangeTile(1);
                        best = Mathf.Max(best, MiniMax(depth + 1, !isMax));

                        board[x, y].Reset();
                    }
                }
            }

            return best;
        }
        else
        {
            int best = 1000;
            for(int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if(board[x, y].PlayedBy() == -1)
                    {
                        board[x, y].ChangeTile(0);
                        best = Mathf.Max(best, MiniMax(depth + 1, !isMax));
                        board[x, y].Reset();
                    }
                }
            }
            return best;
        }

    }

    int Evaluate()
    {
        for(int row = 0; row< 3; row++)
        {
            if(board[row, 0].PlayedBy() == board[row, 1].PlayedBy() &&
                board[row, 1].PlayedBy() == board[row, 2].PlayedBy())
            {
                if (board[row, 0].PlayedBy() == 1)
                    return +10;
                else if (board[row, 0].PlayedBy() == 0)
                    return -10;
            }
        }

        for (int col = 0; col < 3; col++)
        {
            if (board[0, col].PlayedBy() == board[1, col].PlayedBy() &&
                board[1, col].PlayedBy() == board[2, col].PlayedBy())
            {
                if (board[0, col].PlayedBy() == 1)
                    return +10;
                else if (board[0, col].PlayedBy() == 0)
                    return -10;
            }
        }

        if(board[0,0].PlayedBy() == board[1, 1].PlayedBy() && board[1, 1].PlayedBy() == board[2, 2].PlayedBy())
        {
            if (board[0, 0].PlayedBy() == 1)
                return +10;
            else if (board[0, 0].PlayedBy() == 0)
                return -10;
        }

        if (board[0, 2].PlayedBy() == board[1, 1].PlayedBy() && board[1, 1].PlayedBy() == board[2, 0].PlayedBy())
        {
            if (board[0, 2].PlayedBy() == 1)
                return +10;
            else if (board[0, 2].PlayedBy() == 0)
                return -10;
        }

        return 0;
    }

    bool MovesRemaining()
    {
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(board[x, y].PlayedBy() == -1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RestartGame()
    {
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col <3; col++)
            {
                //Debug.Log(row + " " + col);
                board[row, col].Reset();
            }
        }
        victoryText.text = "*****";
        int turn = Random.Range(0, 2);
        if(turn == 0)
        {
            AIMove();
        }

        //turnCounter = 0;
        //topLeft.GetComponent<Image>().sprite = null;
        //topCenter.GetComponent<Image>().sprite = null;
        //topRight.GetComponent<Image>().sprite = null;
        //middleLeft.GetComponent<Image>().sprite = null;
        //middleCenter.GetComponent<Image>().sprite = null;
        //middleRight.GetComponent<Image>().sprite = null;
        //bottomLeft.GetComponent<Image>().sprite = null;
        //bottomCenter.GetComponent<Image>().sprite = null;
        //bottomRight.GetComponent<Image>().sprite = null;

        //topLeft.GetComponent<ButtonCall>().Reset();
        //topCenter.GetComponent<ButtonCall>().Reset();
        //topRight.GetComponent<ButtonCall>().Reset();
        //middleLeft.GetComponent<ButtonCall>().Reset();
        //middleCenter.GetComponent<ButtonCall>().Reset();
        //middleRight.GetComponent<ButtonCall>().Reset();
        //bottomLeft.GetComponent<ButtonCall>().Reset();
        //bottomCenter.GetComponent<ButtonCall>().Reset();
        //bottomRight.GetComponent<ButtonCall>().Reset();
        //bottomRight.GetComponent<ButtonCall>().Reset();

    }

    public Sprite player_Icon() { return player; }
    public Sprite AI_Icon() { return AI; }

    //____________________________________________________________ old code

    //public GameObject FindLastSpace()
    //{
    //    if (!topLeft.GetComponent<ButtonCall>().IsUsed()) return topLeft;
    //    else if (!topCenter.GetComponent<ButtonCall>().IsUsed()) return topCenter;
    //    else if (!topRight.GetComponent<ButtonCall>().IsUsed()) return topRight;
    //    else if (!middleLeft.GetComponent<ButtonCall>().IsUsed()) return middleLeft;
    //    else if (!middleCenter.GetComponent<ButtonCall>().IsUsed()) return middleCenter;
    //    else if (!middleRight.GetComponent<ButtonCall>().IsUsed()) return middleRight;
    //    else if (!bottomLeft.GetComponent<ButtonCall>().IsUsed()) return bottomLeft;
    //    else if (!bottomCenter.GetComponent<ButtonCall>().IsUsed()) return bottomCenter;
    //    else if (!bottomRight.GetComponent<ButtonCall>().IsUsed()) return bottomRight;
    //    else return null; 
    //}
    //public void CheckWin()
    //{
    //    if ((topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && topCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (topLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (topCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (topRight.GetComponent<ButtonCall>().PlayedBy() == 0 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (middleLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 0) ||
    //       (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 0 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 0 && topRight.GetComponent<ButtonCall>().PlayedBy() == 0))
    //    {
    //        victoryText.text = "AI wins";
    //    }

    //    if ((topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && topCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1)||
    //       (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (topCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (topRight.GetComponent<ButtonCall>().PlayedBy() == 1 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        victoryText.text = "Player wins";
    //    }

    //    else
    //    {
    //        victoryText.text = "tie";
    //    }
    //}

    //public void CatchLastMoves()
    //{
    //    List<GameObject> catches = new List<GameObject>();

    //    if ((topLeft.GetComponent<ButtonCall>().IsUsed() && topCenter.GetComponent<ButtonCall>().IsUsed()) ||
    //       (middleRight.GetComponent<ButtonCall>().IsUsed() && bottomRight.GetComponent<ButtonCall>().IsUsed()))
    //    {
    //        if(!selection.Contains(topRight))
    //        selection.Add(topRight);


    //        catches.Add(topRight);
    //    }

    //    if((topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(topCenter))
    //            selection.Add(topCenter);
    //        catches.Add(topCenter);
    //    }

    //    if((topCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(topLeft))
    //            selection.Add(topLeft);
    //        catches.Add(topLeft);
    //    }

    //    if((middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (topRight.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(middleRight))
    //            selection.Add(middleRight);
    //        catches.Add(middleRight);
    //    }

    //    if((middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 1)||
    //       (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //       (bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(middleCenter))
    //            selection.Add(middleCenter);
    //        catches.Add(middleCenter);
    //    }

    //    if((middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && middleRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(middleLeft))
    //            selection.Add(middleLeft);
    //        catches.Add(middleLeft);
    //    }

    //    if((bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (middleRight.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(bottomRight))
    //            selection.Add(bottomRight);
    //        catches.Add(bottomRight);
    //    }

    //    if((bottomLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && topCenter.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(bottomCenter))
    //            selection.Add(bottomCenter);
    //        catches.Add(bottomCenter);
    //    }

    //    if((bottomCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && bottomRight.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (topLeft.GetComponent<ButtonCall>().PlayedBy() == 1 && middleLeft.GetComponent<ButtonCall>().PlayedBy() == 1) ||
    //        (middleCenter.GetComponent<ButtonCall>().PlayedBy() == 1 && topRight.GetComponent<ButtonCall>().PlayedBy() == 1))
    //    {
    //        if (!selection.Contains(bottomLeft))
    //            selection.Add(bottomLeft);
    //        catches.Add(bottomLeft);
    //    }

    //    if (catches.Count > 0)
    //        selection = catches;

    //}

    //AIMove
    //CheckWin();
    //bool played = false;
    //if (turn == 1) 
    //{
    //    turnCounter++;
    //}

    //switch (turnCounter)
    //{
    //    case 0:
    //        selection.Add(topLeft);
    //        selection.Add(topRight);
    //        selection.Add(bottomLeft);
    //        selection.Add(bottomRight);

    //        break;
    //    case 1:
    //        selection.Add(topLeft);
    //        selection.Add(topCenter);
    //        selection.Add(topRight);
    //        selection.Add(middleLeft);
    //        selection.Add(middleRight);
    //        selection.Add(bottomLeft);
    //        selection.Add(bottomCenter);
    //        selection.Add(bottomRight);
    //        if (topLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
    //        if (topCenter.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topCenter);
    //        if (topRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topRight);
    //        if (middleLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(middleLeft);
    //        if (middleRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(middleRight);
    //        if (bottomLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(bottomLeft);
    //        if (bottomCenter.GetComponent<ButtonCall>().IsUsed()) selection.Remove(bottomCenter);
    //        if (bottomRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(bottomRight);
    //        break;
    //    case 2:
    //        selection.Add(topLeft);
    //        selection.Add(topRight);
    //        selection.Add(bottomLeft);
    //        selection.Add(bottomRight);
    //        if (topLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topLeft);
    //        if (topRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(topRight);
    //        if (bottomLeft.GetComponent<ButtonCall>().IsUsed()) selection.Remove(bottomLeft);
    //        if (bottomRight.GetComponent<ButtonCall>().IsUsed()) selection.Remove(bottomRight);
    //        CatchLastMoves();
    //        break;
    //    case 3:
    //        CatchLastMoves();
    //        break;
    //    case 4:
    //        CatchLastMoves();
    //        break;
    //    case 5:
    //        selection.Add(FindLastSpace());
    //        break;
    //}

    //while (!played)
    //{
    //    int spot = Random.Range(0, selection.Count-1);
    //    if (!selection[spot].GetComponent<ButtonCall>().IsUsed())
    //    {
    //        selection[spot].GetComponent<ButtonCall>().ChangeTile(0);
    //        played = true;
    //    }
    //    else
    //    {
    //        selection.RemoveAt(spot);
    //    }
    //}

    //if (turn == 0)
    //{
    //    turnCounter++;
    //}
    //    CheckWin();


}
