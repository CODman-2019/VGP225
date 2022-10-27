using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCall : MonoBehaviour
{
    private bool used;
    private int playedBy;
    
    public void ChangeTile(int side)
    {
        if (!used)
        {
            if(side == 0)
            {
                GetComponent<Image>().sprite = Game.game.player_Icon();
                playedBy = 0;
                Game.game.AIMove();
            }
            else
            {
                GetComponent<Image>().sprite = Game.game.AI_Icon();
                playedBy = 1;
            }
            used = true;

        }
    }

    public int PlayedBy() => playedBy;
    public bool IsUsed() => used;
    public void Reset()
    {
        used = false;
    }
}
