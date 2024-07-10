using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_PlaceConstruction_State : GamePlayState
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

                RaycastHit2D hit1 = Physics2D.Raycast(ray.origin, ray.direction, 100, InputManager.Instance.constructionLayer);

                if (hit1 && hit1.collider.GetComponent<Construction>() != null)
                {
                    if (!hit1.collider.GetComponent<Construction>().isPlaced)
                    {
                        hit1.collider.GetComponent<Construction>().RefillResources();
                        hit1.collider.GetComponent<Construction>().DestroyBeforePlaced();
                        
                    }
                    return;
                }

                if (!CoreManager.Instance.selectingPrefab.IsEnoughResourcesToBuild()) return;

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, InputManager.Instance.tileLayer);
                if (hit && hit.collider.GetComponent<GridTile>() != null)
                {
                    Debug.Log("Tile");
                    InputManager.Instance.PlaceNewConstruction(hit.collider.transform.position, CoreManager.Instance.selectingPrefab.Construction_Width);
                }
                
            }
        }
    }

    public override void OnExit()
    {
        
    }
}
