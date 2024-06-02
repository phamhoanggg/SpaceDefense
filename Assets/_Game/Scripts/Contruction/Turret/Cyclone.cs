using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : Turret, IAirAttackable, ILandAttackable
{
    [SerializeField] private CycloneLaser laserPrefab;
    private CycloneLaser laser;
    [SerializeField] private Transform cannon;
    public override void Attack(float dmg, Transform target)
    {
        if (laser == null)
        {
            laser = Instantiate(laserPrefab);
            laser.OnInit(cannon, target, atk_range, dmg);
        }

        if (!laser.isActiveAndEnabled)
        {
            laser.gameObject.SetActive(true);
            laser.OnInit(cannon, target, atk_range, dmg);
        }
    }
    public void AttackOnAir(float dmg, Transform target)
    {
        
    }

    public void AttackOnLand(float dmg, Transform target)
    {
        
    }
}
