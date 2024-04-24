using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_PlaceContruction_State : GamePlayState
{
    public override void OnEnter()
    {
        
    }

    public override void OnExecute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y < 520) return;

            Ray ray = InputManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (CoreManager.Instance.selectingPrefab != null)
            {
                if (!CoreManager.Instance.selectingPrefab.IsEnoughResourcesToBuild()) return;

                RaycastHit2D hit1 = Physics2D.Raycast(ray.origin, ray.direction, 100, InputManager.Instance.constructionLayer);

                if (hit1 && hit1.collider.GetComponent<Contruction>() != null)
                {
                    if (!hit1.collider.GetComponent<Contruction>().isPlaced)
                    {
                        hit1.collider.GetComponent<Contruction>().RefillResources();
                        hit1.collider.GetComponent<Contruction>().DestroyContruction();
                    }
                    return;
                }


                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, InputManager.Instance.tileLayer);
                if (hit && hit.collider.GetComponent<GridTile>() != null)
                {
                    Debug.Log("Tile");
                    InputManager.Instance.PlaceNewContruction(hit.collider.transform.position, CoreManager.Instance.selectingPrefab.Contruction_Width);
                }
                
            }
        }
    }

    public override void OnExit()
    {
        
    }
}
