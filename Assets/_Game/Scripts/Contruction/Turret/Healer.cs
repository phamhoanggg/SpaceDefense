using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Construction
{
    [SerializeField] private float heal_per_second;
    [SerializeField] private GameObject top_obj;

    public override void Place()
    {
        base.Place();
        top_obj.SetActive(true);
    }
}
