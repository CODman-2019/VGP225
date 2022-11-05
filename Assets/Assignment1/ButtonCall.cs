using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCall : MonoBehaviour
{
    private bool used = false;
    private int playedBy;

    private void Start()
    {
        GetComponent<Image>().sprite = null;
    }

    public void ChangeTile(int side)
    {
        if (!used)
        {
            playedBy = side;
            used = true;


            if(side == 0)
            {
                GetComponent<Image>().sprite = Game.game.AI_Icon();
            }
            else if( side == 1)
            {
                GetComponent<Image>().sprite = Game.game.player_Icon();
                Game.game.AIMove();
            }
        }
    }

    public int PlayedBy() => playedBy;
    public bool IsUsed() => used;
    public void Reset()
    {
        used = false;
        GetComponent<Image>().sprite = null;
        playedBy = -1;
    }
}
