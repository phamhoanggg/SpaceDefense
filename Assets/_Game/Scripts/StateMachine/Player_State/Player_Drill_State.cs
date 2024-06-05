using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Drill_State : PlayerState
{
    public override void OnEnter()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1)
        {
            TutorialController.Instance.NextTutorial(1.5f);
        }
    }
    float counter = 1f;
    public override void OnExecute()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            DataManager.Instance.gameData.resourcesAmounts[(int)Player.Instance.drillingType]++;
            counter = 1f;
        }     
    }
}
