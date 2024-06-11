using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSensor : MonoBehaviour
{
    [SerializeField] private GameObject parentDrill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_START_POSITION))
        {
            gameObject.tag = GameConstant.TAG_END_POSITION;
            if (collision.GetComponent<ConveyorSensor>() != null) collision.GetComponent<ConveyorSensor>().OnTriggerEndTag();
            parentDrill.GetComponent<ISensorInOut>().SetUpOutput(this);
        }

        if (collision.CompareTag(GameConstant.TAG_END_POSITION))
        {
            gameObject.tag = GameConstant.TAG_START_POSITION;
            if (collision.GetComponent<ConveyorSensor>() != null) collision.GetComponent<ConveyorSensor>().OnTriggerStartTag();
            parentDrill.GetComponent<ISensorInOut>().SetUpInput(this);
        }
    }
}
