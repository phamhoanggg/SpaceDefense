using UnityEngine;

public class ConveyorSensor : MonoBehaviour
{
    [SerializeField] Conveyor owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_START_POSITION) && collision.transform.parent.gameObject != owner.gameObject)
        {
            owner.OutputList.Clear();
            owner.OutputList.Add(gameObject);
            owner.UpdateConveyorStyle(false, true);       
        }

        if (collision.CompareTag(GameConstant.TAG_END_POSITION) && collision.transform.parent.gameObject != owner.gameObject)
        {
            if (!owner.InputList.Contains(gameObject))
            {
                owner.InputList.Add(gameObject);
                owner.UpdateConveyorStyle(true, false);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_START_POSITION) && collision.transform.parent.gameObject != owner.gameObject)
        {
            if (owner.OutputList.Contains(gameObject))
            {
                owner.OutputList.Remove(gameObject);
                owner.UpdateConveyorStyle(false, false);
            }
        }

        if (collision.CompareTag(GameConstant.TAG_END_POSITION) && collision.transform.parent.gameObject != owner.gameObject)
        {
            owner.InputList.Remove(gameObject);
            owner.UpdateConveyorStyle(false, false);
        }
    }
}
