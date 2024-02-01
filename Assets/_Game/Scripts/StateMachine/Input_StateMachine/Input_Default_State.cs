using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Default_State : State
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = InputManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, InputManager.Instance.oreLayer);

            if (hit && hit.collider.CompareTag(GameConstant.TAG_ORE))
            {
                Mine ore = hit.collider.GetComponent<Mine>();
                if (Vector2.Distance(ore.transform.position, Player.Instance.transform.position) <= Player.Instance.Drill_Distance)
                {
                    Player.Instance.DisplayDrillLaser(ore.transform);
                    Player.Instance.drillingType = ore.resType;
                }
            }

        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
