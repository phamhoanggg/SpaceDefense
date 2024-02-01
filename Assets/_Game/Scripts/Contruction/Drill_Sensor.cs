using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Sensor : MonoBehaviour
{
    [SerializeField] private Drill parentDrill;

    private Transform tf;
    public Transform TF => tf;
    private void Start()
    {
        tf = transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_START_POSITION))
        {
            parentDrill.outputTransformList.Add(this.tf);
            parentDrill.PlayFanAnim(true);
        }
    }
}
