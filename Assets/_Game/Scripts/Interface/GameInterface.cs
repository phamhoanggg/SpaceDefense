using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Unit Interface
public interface IAirAttackable
{
    public void AttackOnAir(float dmg, Transform target);
}

public interface ILandAttackable
{
    public void AttackOnLand(float dmg, Transform target);
}

public interface IFlyable
{
    public void TakeAirDamage(float dmg);
}

public interface IOnLand
{
    public void TakeLandDamage(float dmg);
}

#endregion
public interface IDriller
{

}

public interface IDrillable
{

}

public interface ISensorInOut
{
    public void SetUpInput(ProductSensor sensor);
    public void SetUpOutput(ProductSensor sensor);
}