using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelWall : Construction
{
    public override void TakeLandDamage(float dmg)
    {
        dmg *= 0.8f;
        base.TakeLandDamage(dmg);
    }
}
