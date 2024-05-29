using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Top : MonoBehaviour
{
    [SerializeField] private float heal_per_second;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Construction>() != null)
        {
            collision.gameObject.GetComponent<Construction>().GetHeal(heal_per_second * Time.deltaTime);
            Debug.Log("Healing " + collision.gameObject.name);
        }
    }
}
