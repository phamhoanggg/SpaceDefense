using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Drill_State : State
{
    float counter = 1.5f;
    public override void OnExecute()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            DataManager.Instance.gameData.resourcesAmounts[(int)Player.Instance.drillingType]++;
            counter = 1.5f;
        }     
    }
}
